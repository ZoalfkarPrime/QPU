using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/SiteRequest")]
public class SiteRequestController(ISiteRequestService siteRequestService) : ControllerBase
{
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var result = await siteRequestService.GetQueryable().ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await siteRequestService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    /// <summary>
    /// Submit an employment (job application) request — طلب توظيف
    /// </summary>
    [HttpPost("Employment")]
    public async Task<IActionResult> CreateEmployment([FromBody] CreateEmploymentRequest model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await siteRequestService.CreateEmploymentAsync(model);
        return Ok(created);
    }

    /// <summary>
    /// Submit a contact-us message — قالب مراسلة
    /// </summary>
    [HttpPost("ContactUs")]
    public async Task<IActionResult> CreateContactUs([FromBody] CreateContactUsRequest model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await siteRequestService.CreateContactUsAsync(model);
        return Ok(created);
    }

    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] SiteRequestDto model)
    {
        SiteRequestDto? updated = null;
        if (ModelState.IsValid)
            updated = await siteRequestService.UpdateAsync(model);
        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] SiteRequestDto model)
    {
        if (ModelState.IsValid)
            await siteRequestService.DeleteAsync(model.Id);
        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }
}
