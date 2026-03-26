using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/ScientificResearch")]
public class ScientificResearchController(IScientificResearchService scientificResearchService) : ControllerBase
{
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var result = await scientificResearchService.GetQueryable().ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await scientificResearchService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost("Create")]
    public async Task<JsonResult> Create(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] CreateScientificResearchRequest model)
    {
        ScientificResearchDto? created = null;
        if (ModelState.IsValid)
            created = await scientificResearchService.CreateAsync(model);
        return new JsonResult(new[] { created }.ToDataSourceResult(request, ModelState));
    }

    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] ScientificResearchDto model)
    {
        ScientificResearchDto? updated = null;
        if (ModelState.IsValid)
            updated = await scientificResearchService.UpdateAsync(model);
        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] ScientificResearchDto model)
    {
        if (ModelState.IsValid)
            await scientificResearchService.DeleteAsync(model.Id);
        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }
}
