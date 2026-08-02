using System.Security.Claims;

namespace QPU.Services;

public class FacultyAccessService(IHttpContextAccessor httpContextAccessor) : IFacultyAccessService
{
    public int? GetScopedFacultyId()
    {
        var user = httpContextAccessor.HttpContext?.User;
        if (user is null || user.Identity?.IsAuthenticated != true)
            return null;

        // SuperAdmin — no restrictions
        if (user.Claims.FirstOrDefault(c => c.Type == "IsSuperAdmin")?.Value == "true")
            return null;

        // Admin role — no faculty restrictions either
        if (user.IsInRole("Admin"))
            return null;

        var facultyClaim = user.FindFirst("FacultyId")?.Value;
        if (string.IsNullOrWhiteSpace(facultyClaim))
            return null; // no faculty scope → unrestricted (e.g. global admin)

        return int.TryParse(facultyClaim, out var id) ? id : null;
    }

    public bool CanAccess(int? facultyId)
    {
        var scoped = GetScopedFacultyId();
        if (scoped is null) return true;          // unrestricted
        if (facultyId is null) return false;       // scoped user trying to access unscoped data
        return scoped == facultyId;
    }
}
