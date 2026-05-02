using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class UserService(
    AppDBContext db,
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    ILogger<UserService> logger) : IUserService, IRoleService
{
    // ── Users ────────────────────────────────────────────────────────────────

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        var users = await db.Users
            .Where(u => !u.IsDeleted)
            .ToListAsync();

        var result = new List<UserDto>();
        foreach (var user in users)
        {
            var roles = await GetUserRolesAsync(user.Id);
            result.Add(MapToDto(user, roles));
        }
        return result;
    }

    public async Task<UserDto?> GetUserByIdAsync(string id)
    {
        var user = await db.Users.FindAsync(id);
        if (user == null) return null;
        var roles = await GetUserRolesAsync(id);
        return MapToDto(user, roles);
    }

    public async Task<(bool Success, string? Error, UserDto? User)> CreateUserAsync(CreateUserRequest request)
    {
        var user = new AppUser
        {
            Email = request.Email,
            UserName = request.UserName,
            NormalizedEmail = request.Email.ToUpper(),
            NormalizedUserName = request.UserName.ToUpper(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            UserType = 2,
            FacultyId = request.FacultyId,
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            LockoutEnabled = false,
            IsDeleted = false,
            IsActive = true,
            IsVerified = true
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            logger.LogWarning("CreateUser failed for {Email}: {Errors}", request.Email, errors);
            return (false, errors, null);
        }

        foreach (var roleId in request.RoleIds)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role?.Name != null)
                await userManager.AddToRoleAsync(user, role.Name);
        }

        logger.LogInformation("User created: {Email}", request.Email);
        var roles = await GetUserRolesAsync(user.Id);
        return (true, null, MapToDto(user, roles));
    }

    public async Task<(bool Success, string? Error, UserDto? User)> UpdateUserAsync(UpdateUserRequest request)
    {
        var user = await userManager.FindByIdAsync(request.Id);
        if (user == null) return (false, "User not found.", null);

        user.Email = request.Email;
        user.UserName = request.UserName;
        user.NormalizedEmail = request.Email.ToUpper();
        user.NormalizedUserName = request.UserName.ToUpper();
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.FacultyId = request.FacultyId;

        var updateResult = await userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            var errors = string.Join(", ", updateResult.Errors.Select(e => e.Description));
            return (false, errors, null);
        }

        // Replace roles
        var currentRoles = await userManager.GetRolesAsync(user);
        await userManager.RemoveFromRolesAsync(user, currentRoles);

        foreach (var roleId in request.RoleIds)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role?.Name != null)
                await userManager.AddToRoleAsync(user, role.Name);
        }

        logger.LogInformation("User updated: {Id}", request.Id);
        var roles = await GetUserRolesAsync(user.Id);
        return (true, null, MapToDto(user, roles));
    }

    public async Task<bool> DeleteUserAsync(string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null) return false;

        user.IsDeleted = true;
        user.IsActive = false;
        await userManager.UpdateAsync(user);
        logger.LogInformation("User soft-deleted: {Id}", id);
        return true;
    }

    public async Task<bool> SetActiveAsync(string id, bool isActive)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user == null) return false;

        user.IsActive = isActive;
        await userManager.UpdateAsync(user);
        logger.LogInformation("User {Id} active set to {IsActive}", id, isActive);
        return true;
    }

    // ── Roles ────────────────────────────────────────────────────────────────

    public async Task<List<RoleDto>> GetAllRolesAsync()
    {
        return await db.Roles
            .Select(r => new RoleDto { Id = r.Id, Name = r.Name ?? string.Empty })
            .ToListAsync();
    }

    public async Task<RoleDto?> GetRoleByIdAsync(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        return role == null ? null : new RoleDto { Id = role.Id, Name = role.Name ?? string.Empty };
    }

    public async Task<(bool Success, string? Error, RoleDto? Role)> CreateRoleAsync(CreateRoleRequest request)
    {
        var role = new AppRole { Name = request.Name, NormalizedName = request.Name.ToUpper() };
        var result = await roleManager.CreateAsync(role);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, errors, null);
        }
        logger.LogInformation("Role created: {Name}", request.Name);
        return (true, null, new RoleDto { Id = role.Id, Name = role.Name ?? string.Empty });
    }

    public async Task<(bool Success, string? Error, RoleDto? Role)> UpdateRoleAsync(UpdateRoleRequest request)
    {
        var role = await roleManager.FindByIdAsync(request.Id);
        if (role == null) return (false, "Role not found.", null);

        role.Name = request.Name;
        role.NormalizedName = request.Name.ToUpper();

        var result = await roleManager.UpdateAsync(role);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return (false, errors, null);
        }
        logger.LogInformation("Role updated: {Id}", request.Id);
        return (true, null, new RoleDto { Id = role.Id, Name = role.Name ?? string.Empty });
    }

    public async Task<bool> DeleteRoleAsync(string id)
    {
        var role = await roleManager.FindByIdAsync(id);
        if (role == null) return false;

        var result = await roleManager.DeleteAsync(role);
        if (!result.Succeeded)
        {
            logger.LogWarning("DeleteRole failed for {Id}: {Errors}", id,
                string.Join(", ", result.Errors.Select(e => e.Description)));
            return false;
        }
        logger.LogInformation("Role deleted: {Id}", id);
        return true;
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private async Task<List<RoleDto>> GetUserRolesAsync(string userId)
    {
        return await db.UserRoles
            .Where(ur => ur.UserId == userId)
            .Join(db.Roles, ur => ur.RoleId, r => r.Id,
                (ur, r) => new RoleDto { Id = r.Id, Name = r.Name ?? string.Empty })
            .ToListAsync();
    }

    private static UserDto MapToDto(AppUser user, List<RoleDto> roles) => new()
    {
        Id = user.Id,
        Email = user.Email ?? string.Empty,
        UserName = user.UserName ?? string.Empty,
        FirstName = user.FirstName,
        LastName = user.LastName,
        PhoneNumber = user.PhoneNumber,
        UserType = user.UserType,
        FacultyId = user.FacultyId,
        IsActive = user.IsActive,
        IsVerified = user.IsVerified,
        IsDeleted = user.IsDeleted,
        Roles = roles
    };
}
