using Microsoft.AspNetCore.Mvc;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/Search")]
public class SearchController(ISearchService searchService) : ControllerBase
{
    /// <summary>
    /// Global search across all entities.
    /// GET api/Search?q=something
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        if (string.IsNullOrWhiteSpace(q) || q.Trim().Length < 2)
            return BadRequest(new { message = "Query must be at least 2 characters." });

        var result = await searchService.SearchAsync(q);
        return Ok(result);
    }
}
