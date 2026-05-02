using QPU.DTOs;

namespace QPU.Services;

public interface IUserService
{
    Task<List<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(string id);
    Task<(bool Success, string? Error, UserDto? User)> CreateUserAsync(CreateUserRequest request);
    Task<(bool Success, string? Error, UserDto? User)> UpdateUserAsync(UpdateUserRequest request);
    Task<bool> DeleteUserAsync(string id);
    Task<bool> SetActiveAsync(string id, bool isActive);
}

public interface IRoleService
{
    Task<List<RoleDto>> GetAllRolesAsync();
    Task<RoleDto?> GetRoleByIdAsync(string id);
    Task<(bool Success, string? Error, RoleDto? Role)> CreateRoleAsync(CreateRoleRequest request);
    Task<(bool Success, string? Error, RoleDto? Role)> UpdateRoleAsync(UpdateRoleRequest request);
    Task<bool> DeleteRoleAsync(string id);
}
