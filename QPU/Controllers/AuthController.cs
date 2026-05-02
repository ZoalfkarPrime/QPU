using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await authService.RegisterAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("Verify")]
    public async Task<IActionResult> Verify([FromBody] VerifyRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await authService.VerifyAsync(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await authService.LoginAsync(request);
        return result.Code switch
        {
            401 => Unauthorized(result),
            403 => StatusCode(403, result),
            404 => NotFound(result),
            _ => result.Success ? Ok(result) : BadRequest(result)
        };
    }

    [AllowAnonymous]
    [HttpPost("LoginByCode")]
    public async Task<IActionResult> LoginByCode([FromBody] LoginByCodeRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await authService.LoginByCodeAsync(request);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [Authorize]
    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        var token = Request.Headers[HeaderNames.Authorization].ToString();
        var result = await authService.LogoutAsync(token);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [Authorize]
    [HttpGet("Me")]
    public IActionResult Me()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value });
        return Ok(claims);
    }
}
