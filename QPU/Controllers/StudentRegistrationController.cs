using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/StudentRegistration")]
public class StudentRegistrationController(IStudentRegistrationService studentRegistrationService, IFacultyAccessService facultyAccess)
    : FacultyScopedController(facultyAccess)
{
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var query = studentRegistrationService.GetQueryable();
        if (ScopedFacultyId.HasValue)
            query = query.Where(x => x.FacultyId == ScopedFacultyId.Value);
        var result = await query.ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await studentRegistrationService.GetByIdAsync(id);
        if (item is null) return NotFound();
        return CheckAccess(item.FacultyId) ?? Ok(item);
    }

    [AllowAnonymous]
    [HttpPost("Create")]
    public async Task<JsonResult> Create(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] CreateStudentRegistrationRequest model)
    {
        StudentRegistrationDto? created = null;
        if (ModelState.IsValid)
        {
            created = await studentRegistrationService.CreateAsync(model);
        }
        return new JsonResult(new[] { created }.ToDataSourceResult(request, ModelState));
    }

    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] StudentRegistrationDto model)
    {
        StudentRegistrationDto? updated = null;
        if (ModelState.IsValid)
        {
            var deny = CheckAccess(model.FacultyId);
            if (deny is not null) return new JsonResult(deny);
            updated = await studentRegistrationService.UpdateAsync(model);
        }
        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] StudentRegistrationDto model)
    {
        if (ModelState.IsValid)
        {
            var deny = CheckAccess(model.FacultyId);
            if (deny is not null) return new JsonResult(deny);
            await studentRegistrationService.DeleteAsync(model.Id);
        }
        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }

    /// <summary>
    /// Advances the registration to the next sequential status (Draft -> Submitted -> UnderReview -> Accepted).
    /// </summary>
    [HttpPost("{id:int}/Advance")]
    public async Task<IActionResult> Advance(int id)
    {
        var item = await studentRegistrationService.GetByIdAsync(id);
        if (item is null) return NotFound();
        var deny = CheckAccess(item.FacultyId);
        if (deny is not null) return deny;

        try
        {
            var updated = await studentRegistrationService.AdvanceStatusAsync(id);
            return Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Cancels the registration. Allowed from any non-terminal status.
    /// </summary>
    [HttpPost("{id:int}/Cancel")]
    public async Task<IActionResult> Cancel(int id)
    {
        var item = await studentRegistrationService.GetByIdAsync(id);
        if (item is null) return NotFound();
        var deny = CheckAccess(item.FacultyId);
        if (deny is not null) return deny;

        try
        {
            var updated = await studentRegistrationService.CancelAsync(id);
            return Ok(updated);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
