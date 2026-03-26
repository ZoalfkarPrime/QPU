using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/Faculty")]
public class FacultyController(IFacultyService facultyService) : ControllerBase
{
    // Used by Kendo Grid for server-side paging, filtering and sorting
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var result = await facultyService.GetQueryable().ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    // Standard REST endpoint for fetching a single item
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var faculty = await facultyService.GetByIdAsync(id);

        if (faculty is null)
            return NotFound();

        return Ok(faculty);
    }

    [HttpPost("Create")]
    public async Task<JsonResult> Create(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] CreateFacultyRequest model)
    {
        FacultyDto? created = null;

        if (ModelState.IsValid)
            created = await facultyService.CreateAsync(model);

        return new JsonResult(new[] { created }.ToDataSourceResult(request, ModelState));
    }

    // Grid sends back the full FacultyDto row it was showing (same as Wattsan Product update)
    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] FacultyDto model)
    {
        FacultyDto? updated = null;

        if (ModelState.IsValid)
            updated = await facultyService.UpdateAsync(model);

        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    // Grid sends back the full FacultyDto row being deleted (same as Wattsan Product destroy)
    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] FacultyDto model)
    {
        if (ModelState.IsValid)
            await facultyService.DeleteAsync(model.Id);

        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }
}
