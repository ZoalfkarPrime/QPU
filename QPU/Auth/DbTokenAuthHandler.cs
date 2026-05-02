using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using QPU_DataAccess.Models;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace QPU.Auth;

public class DbTokenAuthOptions : AuthenticationSchemeOptions { }

public class DbTokenAuthHandler(
    IServiceProvider serviceProvider,
    IOptionsMonitor<DbTokenAuthOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder)
    : AuthenticationHandler<DbTokenAuthOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(HeaderNames.Authorization, out var authHeader))
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization header."));

        var token = authHeader.ToString();

        using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();

        var userToken = db.UserTokens.FirstOrDefault(t => t.Value == token);
        if (userToken == null)
            return Task.FromResult(AuthenticateResult.Fail("Invalid token."));

        var user = db.Users.Find(userToken.UserId);
        if (user == null || !user.IsActive)
            return Task.FromResult(AuthenticateResult.Fail("User not found or inactive."));

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new("UserType", user.UserType?.ToString() ?? "1"),
            new("FacultyId", user.FacultyId?.ToString() ?? string.Empty)
        };

        var roles = db.UserRoles
            .Where(r => r.UserId == user.Id)
            .Join(db.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
            .ToList();

        foreach (var role in roles)
            if (role != null) claims.Add(new Claim(ClaimTypes.Role, role));

        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}
