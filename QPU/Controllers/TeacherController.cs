using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/Teacher")]
public class TeacherController(ITeacherService teacherService, IFacultyTeacherService facultyTeacherService, IFacultyAccessService facultyAccess)
    : FacultyScopedController(facultyAccess)
{
    [AllowAnonymous]
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var query = teacherService.GetQueryable();
        if (ScopedFacultyId.HasValue)
        {
            var facultyTeacherIds = facultyTeacherService.GetQueryable()
                .Where(ft => ft.FacultyId == ScopedFacultyId.Value)
                .Select(ft => ft.TeacherId);
            query = query.Where(t => facultyTeacherIds.Contains(t.Id));
        }
        var result = await query.ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await teacherService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost("Create")]
    public async Task<JsonResult> Create(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] CreateTeacherRequest model)
    {
        TeacherDto? created = null;
        if (ModelState.IsValid)
            created = await teacherService.CreateAsync(model);
        return new JsonResult(new[] { created }.ToDataSourceResult(request, ModelState));
    }

    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] TeacherDto model)
    {
        TeacherDto? updated = null;
        if (ModelState.IsValid)
            updated = await teacherService.UpdateAsync(model);
        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] TeacherDto model)
    {
        if (ModelState.IsValid)
            await teacherService.DeleteAsync(model.Id);
        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }

    [HttpPatch("{id:int}/SetHonor")]
    public async Task<IActionResult> SetHonor(int id, [FromQuery] bool hasHonor)
    {
        var result = await teacherService.SetHonorAsync(id, hasHonor);
        return result is null ? NotFound() : Ok(result);
    }
}
