using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/User")]
public class UserController(IUserService userService) : ControllerBase
{
    [HttpGet("Read")]
    public async Task<IActionResult> Read()
    {
        var users = await userService.GetAllUsersAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await userService.GetUserByIdAsync(id);
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost("Create")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (success, error, user) = await userService.CreateUserAsync(request);
        return success ? Ok(user) : BadRequest(new { error });
    }

    [HttpPut("Update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var (success, error, user) = await userService.UpdateUserAsync(request);
        if (!success && error == "User not found.") return NotFound();
        return success ? Ok(user) : BadRequest(new { error });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await userService.DeleteUserAsync(id);
        return result ? Ok() : NotFound();
    }

    [HttpPatch("{id}/SetActive")]
    public async Task<IActionResult> SetActive(string id, [FromQuery] bool isActive)
    {
        var result = await userService.SetActiveAsync(id, isActive);
        return result ? Ok() : NotFound();
    }
}

[ApiController]
[Route("api/Role")]
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
