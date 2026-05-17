using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPU.Services;

namespace QPU.Controllers;

/// <summary>
/// Base for all controllers that manage faculty-scoped data.
/// Requires authentication. Exposes helpers to check/filter by faculty.
/// </summary>
[Authorize]
public abstract class FacultyScopedController(IFacultyAccessService facultyAccess) : ControllerBase
{
    /// <summary>
    /// Returns the faculty id the current user is restricted to, or null if unrestricted.
    /// </summary>
    protected int? ScopedFacultyId => facultyAccess.GetScopedFacultyId();

    /// <summary>
    /// Returns Forbid() result when the caller is not allowed to touch the given facultyId.
    /// Returns null when access is permitted.
    /// </summary>
    protected IActionResult? CheckAccess(int? facultyId)
        => facultyAccess.CanAccess(facultyId) ? null : Forbid();
}
