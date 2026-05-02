using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class LoginRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    public string? Phone { get; set; }

    // 1 = email activation, 2 = sms activation
    public int ActivationType { get; set; } = 1;
}

public class VerifyRequest
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    public int VerificationCode { get; set; }
}

public class LoginByCodeRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}

public class AuthResult
{
    public bool Success { get; set; }
    public object? Data { get; set; }
    public string? Message { get; set; }
    public int Code { get; set; }

    public AuthResult(bool success, object? data, string? message, int code)
    {
        Success = success;
        Data = data;
        Message = message;
        Code = code;
    }
}

public class UserSessionDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public int? UserType { get; set; }
    public string Token { get; set; } = string.Empty;
    public string? Photo { get; set; }
}
