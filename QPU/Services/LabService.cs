using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class LabService(AppDBContext db) : ILabService
{
    public IQueryable<LabDto> GetQueryable() =>
        db.Labs.Select(l => new LabDto
        {
            Id = l.Id,
            FacultyId = l.FacultyId,
            Name = l.Name,
            Name_AR = l.Name_AR,
            PictureId = l.PictureId,
            Picture = l.Picture == null ? null : new FileManagerNodeDto
            {
                Id = l.Picture.Id,
                Name = l.Picture.Name,
                Name_AR = l.Picture.Name_AR,
                URL = l.Picture.URL,
                Thumbnail = l.Picture.Thumbnail,
                IsFile = l.Picture.IsFile,
                FileType = l.Picture.FileType
            },
            Content = l.Content,
            Content_AR = l.Content_AR,
            IsPublished = l.IsPublished,
            DisplayOrder = l.DisplayOrder,
            IsActive = l.IsActive,
            CreatedAt = l.CreatedAt,
            UpdatedAt = l.UpdatedAt
        });

    public async Task<LabDto?> GetByIdAsync(int id)
    {
        return await GetQueryable().FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<LabDto> CreateAsync(CreateLabRequest request)
    {
        var entity = new Lab
        {
            FacultyId = request.FacultyId,
            Name = request.Name,
            Name_AR = request.Name_AR,
            PictureId = request.PictureId,
            Content = request.Content,
            Content_AR = request.Content_AR,
            IsPublished = request.IsPublished,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.Labs.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<LabDto?> UpdateAsync(LabDto dto)
    {
        var entity = await db.Labs.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.FacultyId = dto.FacultyId;
        entity.Name = dto.Name;
        entity.Name_AR = dto.Name_AR;
        entity.PictureId = dto.PictureId;
        entity.Content = dto.Content;
        entity.Content_AR = dto.Content_AR;
        entity.IsPublished = dto.IsPublished;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.Labs.FindAsync(id);
        if (entity is null) return false;

        db.Labs.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static LabDto ToDto(Lab l) => new()
    {
        Id = l.Id,
        FacultyId = l.FacultyId,
        Name = l.Name,
        Name_AR = l.Name_AR,
        PictureId = l.PictureId,
        Picture = l.Picture == null ? null : new FileManagerNodeDto
        {
            Id = l.Picture.Id,
            Name = l.Picture.Name,
            Name_AR = l.Picture.Name_AR,
            URL = l.Picture.URL,
            Thumbnail = l.Picture.Thumbnail,
            IsFile = l.Picture.IsFile,
            FileType = l.Picture.FileType
        },
        Content = l.Content,
        Content_AR = l.Content_AR,
        IsPublished = l.IsPublished,
        DisplayOrder = l.DisplayOrder,
        IsActive = l.IsActive,
        CreatedAt = l.CreatedAt,
        UpdatedAt = l.UpdatedAt
    };
}
