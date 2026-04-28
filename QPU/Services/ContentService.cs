using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class ContentService(AppDBContext db, IConfiguration config) : IContentService
{
    private string ApiBaseUrl => config["FileManager:APIBaseURL"] ?? string.Empty;

    public IQueryable<ContentDto> GetQueryable()
    {
        var baseUrl = ApiBaseUrl;
        return db.Contents.Select(c => new ContentDto
        {
            Id = c.Id,
            ReferenceId = c.ReferenceId,
            ReferenceType = c.ReferenceType,
            Section = c.Section,
            Title = c.Title,
            ContentMetas = c.ContentMetas
                .OrderBy(cm => cm.DisplayOrder)
                .Select(cm => new ContentMetaDto
                {
                    Id = cm.Id,
                    ContentId = cm.ContentId,
                    Type = cm.Type,
                    KeyName = cm.KeyName,
                    Value = cm.Value,
                    Value_AR = cm.Value_AR,
                    DisplayOrder = cm.DisplayOrder,
                    IsActive = cm.IsActive,
                    CreatedAt = cm.CreatedAt,
                    UpdatedAt = cm.UpdatedAt,
                    Filemanager = (cm.Type == "image" || cm.Type == "video")
                        ? db.FileManagers
                            .Where(f => f.Id.ToString() == cm.Value)
                            .Select(f => new FileManagerNodeDto
                            {
                                Id = f.Id,
                                Name = f.Name,
                                Name_AR = f.Name_AR,
                                URL = f.URL != null ? baseUrl + f.URL : null,
                                Thumbnail = f.Thumbnail != null ? baseUrl + f.Thumbnail : null,
                                IsFile = f.IsFile,
                                FileType = f.FileType
                            })
                            .FirstOrDefault()
                        : null
                })
                .ToList(),
            DisplayOrder = c.DisplayOrder,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        });
    }

    public async Task<ContentDto?> GetByIdAsync(int id)
    {
        return await GetQueryable().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<ContentDto> CreateAsync(CreateContentRequest request)
    {
        var entity = new Content
        {
            ReferenceId = request.ReferenceId,
            ReferenceType = request.ReferenceType,
            Section = request.Section,
            Title = request.Title,
            DisplayOrder = request.DisplayOrder,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.Contents.Add(entity);
        await db.SaveChangesAsync();

        return await GetQueryable().FirstAsync(c => c.Id == entity.Id);
    }

    public async Task<ContentDto?> UpdateAsync(ContentDto dto)
    {
        var entity = await db.Contents.FindAsync(dto.Id);
        if (entity is null)
            return null;

        entity.ReferenceId = dto.ReferenceId;
        entity.ReferenceType = dto.ReferenceType;
        entity.Section = dto.Section;
        entity.Title = dto.Title;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return await GetQueryable().FirstAsync(c => c.Id == entity.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.Contents.FindAsync(id);
        if (entity is null)
            return false;

        db.Contents.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
