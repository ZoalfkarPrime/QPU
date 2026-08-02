using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;
using System.Security.Claims;

namespace QPU.Controllers;

[ApiController]
[Route("api/User")]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("Read")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> Read()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await userService.GetUserByIdAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost("Create")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (success, error, user) = await userService.CreateUserAsync(request);
        return success ? Ok(user) : BadRequest(new { error });
    }

    [HttpPut("Update")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (success, error, user) = await userService.UpdateUserAsync(request);
        if (!success && error == "User not found.") return NotFound();
        return success ? Ok(user) : BadRequest(new { error });
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await userService.DeleteUserAsync(id);
        return result ? Ok() : NotFound();
    }

    [HttpPatch("{id}/SetActive")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> SetActive(string id, [FromQuery] bool isActive)
    {
        var result = await userService.SetActiveAsync(id, isActive);
        return result ? Ok() : NotFound();
    }

    /// <summary>Admin changes any user's password without needing the old one.</summary>
    [HttpPatch("ChangePassword")]
    [Authorize(Policy = "SuperAdminOnly")]
    public async Task<IActionResult> AdminChangePassword([FromBody] AdminChangePasswordRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (success, error) = await userService.AdminChangePasswordAsync(request);
        return success ? Ok() : BadRequest(new { error });
    }

    /// <summary>Authenticated user changes their own password using the old password.</summary>
    [HttpPatch("ChangeMyPassword")]
    public async Task<IActionResult> ChangeMyPassword([FromBody] ChangeMyPasswordRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId)) return Unauthorized();
        var (success, error) = await userService.ChangeMyPasswordAsync(userId, request);
        return success ? Ok() : BadRequest(new { error });
    }
}

[ApiController]
[Route("api/Role")]
[Authorize(Policy = "SuperAdminOnly")]
public class RoleController(IRoleService roleService) : ControllerBase
{
    [HttpGet("Read")]
    public async Task<IActionResult> Read()
    {
        var roles = await roleService.GetAllRolesAsync();
        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var role = await roleService.GetRoleByIdAsync(id);
        return role is null ? NotFound() : Ok(role);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (success, error, role) = await roleService.CreateRoleAsync(request);
        return success ? Ok(role) : BadRequest(new { error });
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateRoleRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (success, error, role) = await roleService.UpdateRoleAsync(request);
        if (!success && error == "Role not found.") return NotFound();
        return success ? Ok(role) : BadRequest(new { error });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await roleService.DeleteRoleAsync(id);
        return result ? Ok() : NotFound();
    }
}
