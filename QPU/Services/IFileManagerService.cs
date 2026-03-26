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
}
