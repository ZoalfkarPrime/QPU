using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class ContentService(AppDBContext db) : IContentService
{
    public IQueryable<ContentDto> GetQueryable() =>
        db.Contents.Select(c => new ContentDto
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
                    UpdatedAt = cm.UpdatedAt
                })
                .ToList(),
            DisplayOrder = c.DisplayOrder,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        });

    public async Task<ContentDto?> GetByIdAsync(int id)
    {
        var entity = await db.Contents
            .AsNoTracking()
            .Include(c => c.ContentMetas)
            .FirstOrDefaultAsync(c => c.Id == id);

        return entity is null ? null : ToDto(entity);
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

        return ToDto(entity);
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
        return ToDto(entity);
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

    private static ContentDto ToDto(Content c) => new()
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
                UpdatedAt = cm.UpdatedAt
            })
            .ToList(),
        DisplayOrder = c.DisplayOrder,
        IsActive = c.IsActive,
        CreatedAt = c.CreatedAt,
        UpdatedAt = c.UpdatedAt
    };
}
