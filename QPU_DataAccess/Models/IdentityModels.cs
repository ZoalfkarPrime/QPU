using Microsoft.AspNetCore.Identity;

namespace QPU_DataAccess.Models;

public class AppUser : IdentityUser<string>
{
    public AppUser()
    {
        Id = Guid.NewGuid().ToString();
    }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? Photo { get; set; }
    public bool IsVerified { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
    public int? UserType { get; set; }
    public int? OneTimePassword { get; set; }
    public int? FacultyId { get; set; }

    public virtual Faculty? Faculty { get; set; }
    public virtual ICollection<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
}

public class AppRole : IdentityRole<string>
{
    public AppRole()
    {
        Id = Guid.NewGuid().ToString();
    }
}

public class AppToken : IdentityUserToken<string>
{
    public string? Device { get; set; }
    public string? Location { get; set; }
    public string? IpAddress { get; set; }
    public bool? Status { get; set; }
    public DateTime? CreatedAt { get; set; }
}

public class AppUserClaim : IdentityUserClaim<string>
{
}

public class AppUserLogin : IdentityUserLogin<string>
{
}

public class AppRoleClaim : IdentityRoleClaim<string>
{
}

public class AppUserRole : IdentityUserRole<string>
{
    public virtual AppUser? User { get; set; }
    public virtual AppRole? Role { get; set; }
}