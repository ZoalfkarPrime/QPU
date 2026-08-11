using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/VisitorCounter")]
public class VisitorCounterController(IVisitorCounterService visitorCounterService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetStats()
    {
        var stats = await visitorCounterService.GetStatsAsync();
        return Ok(stats);
    }

    [AllowAnonymous]
    [HttpPost("Track")]
    public async Task<IActionResult> Track()
    {
        var stats = await visitorCounterService.TrackVisitAsync();
        return Ok(stats);
    }

    [Authorize]
    [HttpPost("Set")]
    public async Task<IActionResult> Set([FromBody] SetVisitorCountRequest request)
    {
        var stats = await visitorCounterService.SetCountAsync(request.TotalVisitors);
        return Ok(stats);
    }
}
