using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class FacultyTeacherService(AppDBContext db) : IFacultyTeacherService
{
    public IQueryable<FacultyTeacherDto> GetQueryable() =>
        db.FacultyTeachers.Select(ft => new FacultyTeacherDto
        {
            Id = ft.Id,
            FacultyId = ft.FacultyId,
            TeacherId = ft.TeacherId,
            DisplayOrder = ft.DisplayOrder,
            IsActive = ft.IsActive,
            CreatedAt = ft.CreatedAt,
            UpdatedAt = ft.UpdatedAt
        });

    public async Task<FacultyTeacherDto?> GetByIdAsync(int id)
    {
        var entity = await db.FacultyTeachers.AsNoTracking().FirstOrDefaultAsync(ft => ft.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<FacultyTeacherDto> CreateAsync(CreateFacultyTeacherRequest request)
    {
        var entity = new FacultyTeacher
        {
            FacultyId = request.FacultyId,
            TeacherId = request.TeacherId,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.FacultyTeachers.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<FacultyTeacherDto?> UpdateAsync(FacultyTeacherDto dto)
    {
        var entity = await db.FacultyTeachers.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.FacultyId = dto.FacultyId;
        entity.TeacherId = dto.TeacherId;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.FacultyTeachers.FindAsync(id);
        if (entity is null) return false;

        db.FacultyTeachers.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static FacultyTeacherDto ToDto(FacultyTeacher ft) => new()
    {
        Id = ft.Id,
        FacultyId = ft.FacultyId,
        TeacherId = ft.TeacherId,
        DisplayOrder = ft.DisplayOrder,
        IsActive = ft.IsActive,
        CreatedAt = ft.CreatedAt,
        UpdatedAt = ft.UpdatedAt
    };
}
