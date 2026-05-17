using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/Lab")]
public class LabController(ILabService labService, IFacultyAccessService facultyAccess)
    : FacultyScopedController(facultyAccess)
{
    [AllowAnonymous]
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var query = labService.GetQueryable();
        if (ScopedFacultyId.HasValue)
            query = query.Where(x => x.FacultyId == ScopedFacultyId.Value);
        var result = await query.ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await labService.GetByIdAsync(id);
        if (item is null) return NotFound();
        return CheckAccess(item.FacultyId) ?? Ok(item);
    }

    [HttpPost("Create")]
    public async Task<JsonResult> Create(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] CreateLabRequest model)
    {
        LabDto? created = null;
        if (ModelState.IsValid)
        {
            var deny = CheckAccess(model.FacultyId);
            if (deny is not null) return new JsonResult(deny);
            created = await labService.CreateAsync(model);
        }
        return new JsonResult(new[] { created }.ToDataSourceResult(request, ModelState));
    }

    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] LabDto model)
    {
        LabDto? updated = null;
        if (ModelState.IsValid)
        {
            var deny = CheckAccess(model.FacultyId);
            if (deny is not null) return new JsonResult(deny);
            updated = await labService.UpdateAsync(model);
        }
        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] LabDto model)
    {
        if (ModelState.IsValid)
        {
            var deny = CheckAccess(model.FacultyId);
            if (deny is not null) return new JsonResult(deny);
            await labService.DeleteAsync(model.Id);
        }
        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }
}
