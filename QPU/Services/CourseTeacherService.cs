using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class CourseTeacherService(AppDBContext db) : ICourseTeacherService
{
    public IQueryable<CourseTeacherDto> GetQueryable() =>
        db.CourseTeachers.Select(ct => new CourseTeacherDto
        {
            Id = ct.Id,
            CourseId = ct.CourseId,
            TeacherId = ct.TeacherId,
            DisplayOrder = ct.DisplayOrder,
            IsActive = ct.IsActive,
            CreatedAt = ct.CreatedAt,
            UpdatedAt = ct.UpdatedAt
        });

    public async Task<CourseTeacherDto?> GetByIdAsync(int id)
    {
        var entity = await db.CourseTeachers.AsNoTracking().FirstOrDefaultAsync(ct => ct.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<CourseTeacherDto> CreateAsync(CreateCourseTeacherRequest request)
    {
        var entity = new CourseTeacher
        {
            CourseId = request.CourseId,
            TeacherId = request.TeacherId,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.CourseTeachers.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<CourseTeacherDto?> UpdateAsync(CourseTeacherDto dto)
    {
        var entity = await db.CourseTeachers.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.CourseId = dto.CourseId;
        entity.TeacherId = dto.TeacherId;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.CourseTeachers.FindAsync(id);
        if (entity is null) return false;

        db.CourseTeachers.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static CourseTeacherDto ToDto(CourseTeacher ct) => new()
    {
        Id = ct.Id,
        CourseId = ct.CourseId,
        TeacherId = ct.TeacherId,
        DisplayOrder = ct.DisplayOrder,
        IsActive = ct.IsActive,
        CreatedAt = ct.CreatedAt,
        UpdatedAt = ct.UpdatedAt
    };
}
