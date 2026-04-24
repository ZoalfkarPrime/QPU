using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class ContentMetaService(AppDBContext db, IConfiguration config) : IContentMetaService
{
    private string ApiBaseUrl => config["FileManager:APIBaseURL"] ?? string.Empty;

    public IQueryable<ContentMetaDto> GetQueryable()
    {
        var baseUrl = ApiBaseUrl;
        return db.ContentMetas.Select(cm => new ContentMetaDto
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
            Filemanager = (cm.Type == "image" || cm.Type == "video" || cm.Type == "file")
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
        });
    }

    public async Task<ContentMetaDto?> GetByIdAsync(int id)
    {
        return await GetQueryable().FirstOrDefaultAsync(cm => cm.Id == id);
    }

    public async Task<ContentMetaDto> CreateAsync(CreateContentMetaRequest request)
    {
        var entity = new ContentMeta
        {
            ContentId = request.ContentId,
            Type = request.Type,
            KeyName = request.KeyName,
            Value = request.Value,
            Value_AR = request.Value_AR,
            DisplayOrder = request.DisplayOrder,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.ContentMetas.Add(entity);
        await db.SaveChangesAsync();

        return await GetQueryable().FirstAsync(cm => cm.Id == entity.Id);
    }

    public async Task<ContentMetaDto?> UpdateAsync(ContentMetaDto dto)
    {
        var entity = await db.ContentMetas.FindAsync(dto.Id);
        if (entity is null)
            return null;

        entity.ContentId = dto.ContentId;
        entity.Type = dto.Type;
        entity.KeyName = dto.KeyName;
        entity.Value = dto.Value;
        entity.Value_AR = dto.Value_AR;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return await GetQueryable().FirstAsync(cm => cm.Id == entity.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.ContentMetas.FindAsync(id);
        if (entity is null)
            return false;

        db.ContentMetas.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
