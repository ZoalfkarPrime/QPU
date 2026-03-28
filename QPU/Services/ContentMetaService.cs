using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class ContentMetaService(AppDBContext db) : IContentMetaService
{
    public IQueryable<ContentMetaDto> GetQueryable() =>
        db.ContentMetas.Select(cm => new ContentMetaDto
        {
            Id = cm.Id,
            ContentId = cm.ContentId,
            Type = cm.Type,
            KeyName = cm.KeyName,
            Value = cm.Value,
            DisplayOrder = cm.DisplayOrder,
            IsActive = cm.IsActive,
            CreatedAt = cm.CreatedAt,
            UpdatedAt = cm.UpdatedAt
        });

    public async Task<ContentMetaDto?> GetByIdAsync(int id)
    {
        var entity = await db.ContentMetas.AsNoTracking().FirstOrDefaultAsync(cm => cm.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<ContentMetaDto> CreateAsync(CreateContentMetaRequest request)
    {
        var entity = new ContentMeta
        {
            ContentId = request.ContentId,
            Type = request.Type,
            KeyName = request.KeyName,
            Value = request.Value,
            DisplayOrder = request.DisplayOrder,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.ContentMetas.Add(entity);
        await db.SaveChangesAsync();

        return ToDto(entity);
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
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
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

    private static ContentMetaDto ToDto(ContentMeta cm) => new()
    {
        Id = cm.Id,
        ContentId = cm.ContentId,
        Type = cm.Type,
        KeyName = cm.KeyName,
        Value = cm.Value,
        DisplayOrder = cm.DisplayOrder,
        IsActive = cm.IsActive,
        CreatedAt = cm.CreatedAt,
        UpdatedAt = cm.UpdatedAt
    };
}
