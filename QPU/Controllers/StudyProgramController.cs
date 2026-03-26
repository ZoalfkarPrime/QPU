using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/StudyProgram")]
public class StudyProgramController(IStudyProgramService studyProgramService) : ControllerBase
{
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var result = await studyProgramService.GetQueryable().ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await studyProgramService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost("Create")]
    public async Task<JsonResult> Create(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] CreateStudyProgramRequest model)
    {
        StudyProgramDto? created = null;
        if (ModelState.IsValid)
            created = await studyProgramService.CreateAsync(model);
        return new JsonResult(new[] { created }.ToDataSourceResult(request, ModelState));
    }

    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] StudyProgramDto model)
    {
        StudyProgramDto? updated = null;
        if (ModelState.IsValid)
            updated = await studyProgramService.UpdateAsync(model);
        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] StudyProgramDto model)
    {
        if (ModelState.IsValid)
            await studyProgramService.DeleteAsync(model.Id);
        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }
}
