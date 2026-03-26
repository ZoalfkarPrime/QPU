using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using QPU.DTOs;
using QPU.Services;

namespace QPU.Controllers;

[ApiController]
[Route("api/FileManager")]
public class FileManagerController(IFileManagerService fileManagerService) : ControllerBase
{
    [HttpGet("Read")]
    public async Task<JsonResult> Read([DataSourceRequest] DataSourceRequest request)
    {
        var result = await fileManagerService.GetQueryable().ToDataSourceResultAsync(request);
        return new JsonResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await fileManagerService.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    // Accepts multipart/form-data — files for upload, or Name/ParentId for folder creation
    [HttpPost("Create")]
    public async Task<JsonResult> Create(
        [DataSourceRequest] DataSourceRequest request,
        [FromForm] UploadFileManagerRequest model)
    {
        if (ModelState.IsValid)
            await fileManagerService.InsertAsync(model, ModelState);

        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }

    // Only renames or moves (updates Name and ParentId)
    [HttpPut("Update")]
    public async Task<JsonResult> Update(
        [DataSourceRequest] DataSourceRequest request,
        [FromBody] UpdateFileManagerRequest model)
    {
        if (ModelState.IsValid)
            await fileManagerService.UpdateAsync(model, ModelState);

        return new JsonResult(new[] { model }.ToDataSourceResult(request, ModelState));
    }

    // id comes from query string — same as Wattsan's Destroy endpoint
    [HttpDelete("Delete")]
    public async Task<JsonResult> Delete(
        [DataSourceRequest] DataSourceRequest request,
        Guid id)
    {
        if (ModelState.IsValid)
            await fileManagerService.DeleteAsync(id, ModelState);

        return new JsonResult(new[] { id }.ToDataSourceResult(request, ModelState));
    }
}
