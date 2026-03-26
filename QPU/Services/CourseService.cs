using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class CourseService(AppDBContext db) : ICourseService
{
    public IQueryable<CourseDto> GetQueryable() =>
        db.Courses.Select(c => new CourseDto
        {
            Id = c.Id,
            FacultyId = c.FacultyId,
            StudyYearId = c.StudyYearId,
            Name = c.Name,
            Description = c.Description,
            IsPublished = c.IsPublished,
            DisplayOrder = c.DisplayOrder,
            IsActive = c.IsActive,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        });

    public async Task<CourseDto?> GetByIdAsync(int id)
    {
        var entity = await db.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<CourseDto> CreateAsync(CreateCourseRequest request)
    {
        var entity = new Course
        {
            FacultyId = request.FacultyId,
            StudyYearId = request.StudyYearId,
            Name = request.Name,
            Description = request.Description,
            IsPublished = request.IsPublished,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.Courses.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<CourseDto?> UpdateAsync(CourseDto dto)
    {
        var entity = await db.Courses.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.FacultyId = dto.FacultyId;
        entity.StudyYearId = dto.StudyYearId;
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.IsPublished = dto.IsPublished;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.Courses.FindAsync(id);
        if (entity is null) return false;

        db.Courses.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static CourseDto ToDto(Course c) => new()
    {
        Id = c.Id,
        FacultyId = c.FacultyId,
        StudyYearId = c.StudyYearId,
        Name = c.Name,
        Description = c.Description,
        IsPublished = c.IsPublished,
        DisplayOrder = c.DisplayOrder,
        IsActive = c.IsActive,
        CreatedAt = c.CreatedAt,
        UpdatedAt = c.UpdatedAt
    };
}
