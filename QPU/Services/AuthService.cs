using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class AuthService(
    AppDBContext db,
    UserManager<AppUser> userManager,
    IConfiguration config,
    IHttpContextAccessor httpContextAccessor,
    ILogger<AuthService> logger) : IAuthService
{
    private string ApiBaseUrl => config["FileManager:APIBaseURL"] ?? string.Empty;

    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {
        var existing = await userManager.FindByEmailAsync(request.Email);
        if (existing != null)
            return new AuthResult(false, null, "Email is already registered.", 409);

        var otp = GenerateOtp();

        var user = new AppUser
        {
            Email = request.Email,
            UserName = request.Email,
            NormalizedEmail = request.Email.ToUpper(),
            NormalizedUserName = request.Email.ToUpper(),
            FirstName = request.FullName,
            LastName = string.Empty,
            PhoneNumber = request.Phone,
            OneTimePassword = int.Parse(otp),
            UserType = 1,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            LockoutEnabled = false,
            IsDeleted = false,
            IsActive = false,
            IsVerified = false
        };

        var result = await userManager.CreateAsync(user, otp);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            logger.LogWarning("Register failed for {Email}: {Errors}", request.Email, errors);
            return new AuthResult(false, null, errors, 400);
        }

        logger.LogInformation("User registered: {Email}", request.Email);

        var responseData = new { id = user.Id, email = user.Email, firstName = user.FirstName };
        return new AuthResult(true, responseData, "Registration successful. Use the OTP to verify your account.", 0);
    }

    public async Task<AuthResult> VerifyAsync(VerifyRequest request)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        if (user == null)
            return new AuthResult(false, null, "User not found.", 404);

        if (user.OneTimePassword != request.VerificationCode)
            return new AuthResult(false, null, "Verification code is incorrect.", 400);

        user.IsVerified = true;
        user.IsActive = true;
        user.OneTimePassword = null;

        await userManager.UpdateAsync(user);
        logger.LogInformation("User verified: {Id}", request.Id);

        return new AuthResult(true, "Verified", null, 0);
    }

    public async Task<AuthResult> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new AuthResult(false, null, "Email is not registered.", 404);

        if (!user.EmailConfirmed || !user.IsVerified)
            return new AuthResult(false, new { email = user.Email }, "Account is not verified.", 3);

        if (!user.IsActive)
            return new AuthResult(false, null, "Account is inactive.", 403);

        var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
            return new AuthResult(false, null, "Email or password is incorrect.", 401);

        var token = GenerateToken();
        var device = GetDeviceInfo();

        var userToken = new AppToken
        {
            UserId = user.Id,
            LoginProvider = "QPUSite",
            Name = "AuthToken",
            Value = token,
            Device = device,
            IpAddress = GetIpAddress(),
            Status = true,
            CreatedAt = DateTime.UtcNow
        };

        db.UserTokens.Add(userToken);
        await db.SaveChangesAsync();

        logger.LogInformation("User logged in: {Email}", request.Email);

        var session = new UserSessionDto
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName,
            Phone = user.PhoneNumber,
            UserType = user.UserType,
            Token = token,
            Photo = user.Photo != null ? ApiBaseUrl + user.Photo : null
        };

        return new AuthResult(true, session, null, 0);
    }

    public async Task<AuthResult> LoginByCodeAsync(LoginByCodeRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return new AuthResult(false, null, "Email is not registered.", 404);

        var otp = GenerateOtp();

        user.OneTimePassword = int.Parse(otp);
        var result = await userManager.RemovePasswordAsync(user);
        await userManager.AddPasswordAsync(user, otp);

        logger.LogInformation("LoginByCode OTP generated for {Email}: {OTP}", request.Email, otp);

        return new AuthResult(true, new { id = user.Id, email = user.Email, firstName = user.FirstName }, "Verification code generated.", 0);
    }

    public async Task<AuthResult> LogoutAsync(string token)
    {
        var userToken = await db.UserTokens.FirstOrDefaultAsync(t => t.Value == token);
        if (userToken == null)
            return new AuthResult(false, null, "Token not found.", 404);

        db.UserTokens.Remove(userToken);
        await db.SaveChangesAsync();

        logger.LogInformation("Token revoked: {UserId}", userToken.UserId);
        return new AuthResult(true, null, "Logged out successfully.", 0);
    }

    private static string GenerateOtp()
    {
        return new Random().Next(0, 1000000).ToString("D6");
    }

    private static string GenerateToken()
    {
        return DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() + "_" + Guid.NewGuid().ToString("N");
    }

    private string GetIpAddress()
    {
        return httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    }

    private string GetDeviceInfo()
    {
        var userAgent = httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString();
        return userAgent ?? "Unknown";
    }
}
