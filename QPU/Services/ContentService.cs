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
            Type = c.Type,
            Title = c.Title,
            DisplayOrder = c.DisplayOrder,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        });

    public async Task<ContentDto?> GetByIdAsync(int id)
    {
        var entity = await db.Contents.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<ContentDto> CreateAsync(CreateContentRequest request)
    {
        var entity = new Content
        {
            ReferenceId = request.ReferenceId,
            ReferenceType = request.ReferenceType,
            Type = request.Type,
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
        entity.Type = dto.Type;
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
        Type = c.Type,
        Title = c.Title,
        DisplayOrder = c.DisplayOrder,
        IsActive = c.IsActive,
        CreatedAt = c.CreatedAt,
        UpdatedAt = c.UpdatedAt
    };
}
