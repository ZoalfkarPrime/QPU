using QPU.DTOs;

namespace QPU.Services;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(RegisterRequest request);
    Task<AuthResult> VerifyAsync(VerifyRequest request);
    Task<AuthResult> LoginAsync(LoginRequest request);
    Task<AuthResult> LoginByCodeAsync(LoginByCodeRequest request);
    Task<AuthResult> LogoutAsync(string token);
}
