using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/Gallery")]
public class GalleryController(IGalleryService galleryService) : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var result = await galleryService.GetQueryable().ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await galleryService.GetByIdAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost("Create")]
    public async Task<JsonResult> Create(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] CreateGalleryRequest model)
    {
        GalleryDto? created = null;
        if (ModelState.IsValid)
            created = await galleryService.CreateAsync(model);

        return new JsonResult(new[] { created }.ToDataSourceResult(request, ModelState));
    }

    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] GalleryDto model)
    {
        GalleryDto? updated = null;
        if (ModelState.IsValid)
            updated = await galleryService.UpdateAsync(model);

        return new JsonResult(new[] { updated ?? model }.ToDataSourceResult(request, ModelState));
    }

    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] GalleryDto model)
    {
        if (ModelState.IsValid)
            await galleryService.DeleteAsync(model.Id);

        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }
}
