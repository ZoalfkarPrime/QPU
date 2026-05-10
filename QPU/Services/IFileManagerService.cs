using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QPU.DTOs;

namespace QPU.Services;

public interface IFileManagerService
{
    IQueryable<FileManagerDto> GetQueryable();
    Task<FileManagerDto?> GetByIdAsync(Guid id);
    Task InsertAsync(UploadFileManagerRequest request, ModelStateDictionary modelState);
    Task UpdateAsync(UpdateFileManagerRequest request, ModelStateDictionary modelState);
    Task DeleteAsync(Guid id, ModelStateDictionary modelState);

    /// <summary>
    /// Upload a single file and return the saved FileManager record.
    /// Used by public-facing forms (e.g. CV upload before submitting employment request).
    /// </summary>
    Task<(bool Success, string? Error, FileManagerNodeDto? File)> UploadSingleAsync(IFormFile file, Guid? parentId = null);
    Task<Guid> GetOrCreateFolderAsync(string name);
}
