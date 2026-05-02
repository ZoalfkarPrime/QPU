using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class UserDto
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public int? UserType { get; set; }
    public int? FacultyId { get; set; }
    public bool IsActive { get; set; }
    public bool IsVerified { get; set; }
    public bool IsDeleted { get; set; }
    public List<RoleDto> Roles { get; set; } = [];
}

public class CreateUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;

    public int? FacultyId { get; set; }
    public List<string> RoleIds { get; set; } = [];
}

public class UpdateUserRequest
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }
    public int? FacultyId { get; set; }

    public List<string> RoleIds { get; set; } = [];
}

public class RoleDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}

public class CreateRoleRequest
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}

public class UpdateRoleRequest
{
    [Required]
    public string Id { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}
