using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/Content")]
public class ContentController(IContentService contentService) : ControllerBase
{
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var result = await contentService.GetQueryable().ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await contentService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost("Create")]
    public async Task<JsonResult> Create([DataSourceRequest] DataSourceRequest request, [FromBody] CreateContentRequest model)
    {
        ContentDto? created = null;

        if (ModelState.IsValid)
            created = await contentService.CreateAsync(model);

        return new JsonResult(new[] { created }.ToDataSourceResult(request, ModelState));
    }

    [HttpPut("Update")]
    public async Task<JsonResult> Update([DataSourceRequest] DataSourceRequest request, [FromBody] ContentDto model)
    {
        ContentDto? updated = null;

        if (ModelState.IsValid)
            updated = await contentService.UpdateAsync(model);

        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete([DataSourceRequest] DataSourceRequest request, [FromBody] ContentDto model)
    {
        if (ModelState.IsValid)
            await contentService.DeleteAsync(model.Id);

        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }
}
