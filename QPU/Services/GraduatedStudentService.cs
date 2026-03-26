using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class GraduatedStudentService(AppDBContext db) : IGraduatedStudentService
{
    public IQueryable<GraduatedStudentDto> GetQueryable() =>
        db.GraduatedStudents.Select(g => new GraduatedStudentDto
        {
            Id = g.Id,
            StudyYearId = g.StudyYearId,
            FacultyId = g.FacultyId,
            FullName = g.FullName,
            Average = g.Average,
            StudentNumber = g.StudentNumber,
            IsPublished = g.IsPublished,
            DisplayOrder = g.DisplayOrder,
            IsActive = g.IsActive,
            CreatedAt = g.CreatedAt,
            UpdatedAt = g.UpdatedAt
        });

    public async Task<GraduatedStudentDto?> GetByIdAsync(int id)
    {
        var entity = await db.GraduatedStudents.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<GraduatedStudentDto> CreateAsync(CreateGraduatedStudentRequest request)
    {
        var entity = new GraduatedStudent
        {
            StudyYearId = request.StudyYearId,
            FacultyId = request.FacultyId,
            FullName = request.FullName,
            Average = request.Average,
            StudentNumber = request.StudentNumber,
            IsPublished = request.IsPublished,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.GraduatedStudents.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<GraduatedStudentDto?> UpdateAsync(GraduatedStudentDto dto)
    {
        var entity = await db.GraduatedStudents.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.StudyYearId = dto.StudyYearId;
        entity.FacultyId = dto.FacultyId;
        entity.FullName = dto.FullName;
        entity.Average = dto.Average;
        entity.StudentNumber = dto.StudentNumber;
        entity.IsPublished = dto.IsPublished;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.GraduatedStudents.FindAsync(id);
        if (entity is null) return false;

        db.GraduatedStudents.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static GraduatedStudentDto ToDto(GraduatedStudent g) => new()
    {
        Id = g.Id,
        StudyYearId = g.StudyYearId,
        FacultyId = g.FacultyId,
        FullName = g.FullName,
        Average = g.Average,
        StudentNumber = g.StudentNumber,
        IsPublished = g.IsPublished,
        DisplayOrder = g.DisplayOrder,
        IsActive = g.IsActive,
        CreatedAt = g.CreatedAt,
        UpdatedAt = g.UpdatedAt
    };
}
