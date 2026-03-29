using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class LectureService(AppDBContext db) : ILectureService
{
    public IQueryable<LectureDto> GetQueryable() =>
        db.Lectures.Select(l => new LectureDto
        {
            Id = l.Id,
            CourseId = l.CourseId,
            TeacherId = l.TeacherId,
            Title = l.Title,
            Title_AR = l.Title_AR,
            Content = l.Content,
            Content_AR = l.Content_AR,
            FileId = l.FileId,
            LectureNumber = l.LectureNumber,
            IsPublished = l.IsPublished,
            DisplayOrder = l.DisplayOrder,
            IsActive = l.IsActive,
            CreatedAt = l.CreatedAt,
            UpdatedAt = l.UpdatedAt
        });

    public async Task<LectureDto?> GetByIdAsync(int id)
    {
        var entity = await db.Lectures.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<LectureDto> CreateAsync(CreateLectureRequest request)
    {
        var entity = new Lecture
        {
            CourseId = request.CourseId,
            TeacherId = request.TeacherId,
            Title = request.Title,
            Title_AR = request.Title_AR,
            Content = request.Content,
            Content_AR = request.Content_AR,
            FileId = request.FileId,
            LectureNumber = request.LectureNumber,
            IsPublished = request.IsPublished,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.Lectures.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<LectureDto?> UpdateAsync(LectureDto dto)
    {
        var entity = await db.Lectures.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.CourseId = dto.CourseId;
        entity.TeacherId = dto.TeacherId;
        entity.Title = dto.Title;
        entity.Title_AR = dto.Title_AR;
        entity.Content = dto.Content;
        entity.Content_AR = dto.Content_AR;
        entity.FileId = dto.FileId;
        entity.LectureNumber = dto.LectureNumber;
        entity.IsPublished = dto.IsPublished;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.Lectures.FindAsync(id);
        if (entity is null) return false;

        db.Lectures.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static LectureDto ToDto(Lecture l) => new()
    {
        Id = l.Id,
        CourseId = l.CourseId,
        TeacherId = l.TeacherId,
        Title = l.Title,
        Title_AR = l.Title_AR,
        Content = l.Content,
        Content_AR = l.Content_AR,
        FileId = l.FileId,
        LectureNumber = l.LectureNumber,
        IsPublished = l.IsPublished,
        DisplayOrder = l.DisplayOrder,
        IsActive = l.IsActive,
        CreatedAt = l.CreatedAt,
        UpdatedAt = l.UpdatedAt
    };
}
