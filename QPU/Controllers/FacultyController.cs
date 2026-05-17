using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/Faculty")]
public class FacultyController(IFacultyService facultyService, IFacultyAccessService facultyAccess)
    : FacultyScopedController(facultyAccess)
{
    [AllowAnonymous]
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var query = facultyService.GetQueryable();
        if (ScopedFacultyId.HasValue)
            query = query.Where(f => f.Id == ScopedFacultyId.Value);
        var result = await query.ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var faculty = await facultyService.GetByIdAsync(id);
        if (faculty is null) return NotFound();
        return CheckAccess(faculty.Id) ?? Ok(faculty);
    }

    [HttpPost("Create")]
    public async Task<JsonResult> Create(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] CreateFacultyRequest model)
    {
        FacultyDto? created = null;
        if (ModelState.IsValid)
        {
            // Only unrestricted admins may create new faculties
            var deny = CheckAccess(null);
            if (deny is not null) return new JsonResult(deny);
            created = await facultyService.CreateAsync(model);
        }
        return new JsonResult(new[] { created }.ToDataSourceResult(request, ModelState));
    }

    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] FacultyDto model)
    {
        FacultyDto? updated = null;
        if (ModelState.IsValid)
        {
            var deny = CheckAccess(model.Id);
            if (deny is not null) return new JsonResult(deny);
            updated = await facultyService.UpdateAsync(model);
        }
        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] FacultyDto model)
    {
        if (ModelState.IsValid)
        {
            var deny = CheckAccess(null);
            if (deny is not null) return new JsonResult(deny);
            await facultyService.DeleteAsync(model.Id);
        }
        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }
}
