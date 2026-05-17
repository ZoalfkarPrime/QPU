using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/Lecture")]
public class LectureController(ILectureService lectureService, ICourseService courseService) : ControllerBase
{
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request, [FromQuery] int? facultyId)
    {
        var query = lectureService.GetQueryable();

        if (facultyId.HasValue)
        {
            var courseIds = courseService.GetQueryable()
                .Where(c => c.FacultyId == facultyId.Value)
                .Select(c => c.Id);

            query = query.Where(l => courseIds.Contains(l.CourseId));
        }

        var result = await query.ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await lectureService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost("Create")]
    public async Task<JsonResult> Create(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] CreateLectureRequest model)
    {
        LectureDto? created = null;
        if (ModelState.IsValid)
            created = await lectureService.CreateAsync(model);
        return new JsonResult(new[] { created }.ToDataSourceResult(request, ModelState));
    }

    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] LectureDto model)
    {
        LectureDto? updated = null;
        if (ModelState.IsValid)
            updated = await lectureService.UpdateAsync(model);
        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] LectureDto model)
    {
        if (ModelState.IsValid)
            await lectureService.DeleteAsync(model.Id);
        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }
}
