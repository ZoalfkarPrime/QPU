namespace QPU.Services;

/// <summary>
/// Resolves the current user's faculty scope from claims.
/// - IsSuperAdmin or no FacultyId claim  → null  (unrestricted access)
/// - Has FacultyId claim                 → that int  (scoped to one faculty)
/// </summary>
public interface IFacultyAccessService
{
    /// <summary>Returns null if the caller has unrestricted access, or the specific FacultyId they are locked to.</summary>
    int? GetScopedFacultyId();

    /// <summary>Returns true if the caller may read/write data belonging to the given facultyId.</summary>
    bool CanAccess(int? facultyId);
}
