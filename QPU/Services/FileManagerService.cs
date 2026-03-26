using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class FileManagerService(AppDBContext db, IConfiguration config) : IFileManagerService
{
    private static readonly HashSet<string> ImageExtensions =
        [".jpg", ".jpeg", ".png", ".gif", ".webp", ".bmp"];

    private string UploadPath
    {
        get
        {
            var configuredPath = config["FileManager:UploadPath"];
            if (string.IsNullOrWhiteSpace(configuredPath))
                return Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            return configuredPath.Replace("upolads", "uploads", StringComparison.OrdinalIgnoreCase);
        }
    }

    private string UploadRelativePath => config["FileManager:UploadRelativePath"] ?? "/uploads/";
    private string ApiBaseUrl => config["FileManager:APIBaseURL"] ?? string.Empty;
    private long MaxFileSize => long.Parse(config["FileManager:MaxFileSize"] ?? "52428800");

    public IQueryable<FileManagerDto> GetQueryable()
    {
        var baseUrl = ApiBaseUrl;

        return db.FileManagers.Select(f => new FileManagerDto
        {
            Id = f.Id,
            Name = f.Name,
            URL = f.URL != null ? baseUrl + f.URL : null,
            IsFile = f.IsFile,
            Thumbnail = f.Thumbnail != null ? baseUrl + f.Thumbnail : null,
            FileType = f.FileType,
            ParentId = f.ParentId,
            DisplayOrder = f.DisplayOrder,
            IsActive = f.IsActive,
            CreatedAt = f.CreatedAt,
            UpdatedAt = f.UpdatedAt
        });
    }

    public async Task<FileManagerDto?> GetByIdAsync(Guid id)
    {
        var entity = await db.FileManagers.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        if (entity is null) return null;

        return ToDto(entity, ApiBaseUrl);
    }

    public async Task InsertAsync(UploadFileManagerRequest request, ModelStateDictionary modelState)
    {
        try
        {
            if (request.IsFile)
            {
                if (request.Files is null || request.Files.Count == 0)
                {
                    modelState.AddModelError("Files", "No files provided.");
                    return;
                }

                Directory.CreateDirectory(UploadPath);

                foreach (var file in request.Files)
                {
                    if (file.Length == 0) continue;

                    if (file.Length > MaxFileSize)
                    {
                        modelState.AddModelError("Files", $"{file.FileName} exceeds the maximum allowed size.");
                        return;
                    }

                    var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                    var uniqueName = $"{Guid.NewGuid()}{extension}";
                    var filePath = Path.Combine(UploadPath, uniqueName);

                    await using (var stream = new FileStream(filePath, FileMode.Create))
                        await file.CopyToAsync(stream);

                    var relativeUrl = UploadRelativePath + uniqueName;
                    var thumbnail = ImageExtensions.Contains(extension) ? relativeUrl : null;

                    var entity = new FileManager
                    {
                        Id = Guid.NewGuid(),
                        Name = file.FileName,
                        URL = relativeUrl,
                        IsFile = true,
                        FileType = GetFileType(extension),
                        Thumbnail = thumbnail,
                        ParentId = request.ParentId,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    };

                    db.FileManagers.Add(entity);
                }

                await db.SaveChangesAsync();
            }
            else
            {
                // Folder creation
                var entity = new FileManager
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name ?? "New Folder",
                    IsFile = false,
                    FileType = 0,
                    Thumbnail = request.Thumbnail,
                    ParentId = request.ParentId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                db.FileManagers.Add(entity);
                await db.SaveChangesAsync();
                request.Id = entity.Id;
            }
        }
        catch (Exception ex)
        {
            modelState.AddModelError("FileUpload", ex.Message);
        }
    }

    public async Task UpdateAsync(UpdateFileManagerRequest request, ModelStateDictionary modelState)
    {
        var entity = await db.FileManagers.FindAsync(request.Id);

        if (entity is null)
        {
            modelState.AddModelError("Id", "File or folder not found.");
            return;
        }

        entity.Name = request.Name ?? entity.Name;
        entity.ParentId = request.ParentId;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id, ModelStateDictionary modelState)
    {
        var entity = await db.FileManagers.FindAsync(id);

        if (entity is null) return;

        db.FileManagers.Remove(entity);
        await db.SaveChangesAsync();

        if (entity.IsFile && entity.URL is not null)
        {
            var fileName = Path.GetFileName(entity.URL);
            var filePath = Path.Combine(UploadPath, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }

    private static int GetFileType(string extension) => extension switch
    {
        ".jpg" or ".jpeg" or ".png" or ".gif" or ".webp" or ".bmp" => 1,
        ".pdf" => 2,
        ".doc" or ".docx" => 3,
        ".xls" or ".xlsx" => 4,
        ".mp4" or ".avi" or ".mov" => 5,
        _ => 0
    };



    private static FileManagerDto ToDto(FileManager f, string baseUrl) => new()
    {
        Id = f.Id,
        Name = f.Name,
        URL = f.URL != null ? baseUrl + f.URL : null,
        IsFile = f.IsFile,
        Thumbnail = f.Thumbnail != null ? baseUrl + f.Thumbnail : null,
        FileType = f.FileType,
        ParentId = f.ParentId,
        DisplayOrder = f.DisplayOrder,
        IsActive = f.IsActive,
        CreatedAt = f.CreatedAt,
        UpdatedAt = f.UpdatedAt
    };
}
