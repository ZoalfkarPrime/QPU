using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class TeacherService(AppDBContext db) : ITeacherService
{
    public IQueryable<TeacherDto> GetQueryable() =>
        db.Teachers.Select(t => new TeacherDto
        {
            Id = t.Id,
            Name = t.Name,
            PictureId = t.PictureId,
            Position = t.Position,
            Specialist = t.Specialist,
            ScientificDegree = t.ScientificDegree,
            AcademicDegree = t.AcademicDegree,
            Certificates = t.Certificates,
            Experiences = t.Experiences,
            CvEnglishId = t.CvEnglishId,
            CvArabicId = t.CvArabicId,
            IsPublished = t.IsPublished,
            DisplayOrder = t.DisplayOrder,
            IsActive = t.IsActive,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        });

    public async Task<TeacherDto?> GetByIdAsync(int id)
    {
        var entity = await db.Teachers.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<TeacherDto> CreateAsync(CreateTeacherRequest request)
    {
        var entity = new Teacher
        {
            Name = request.Name,
            PictureId = request.PictureId,
            Position = request.Position,
            Specialist = request.Specialist,
            ScientificDegree = request.ScientificDegree,
            AcademicDegree = request.AcademicDegree,
            Certificates = request.Certificates,
            Experiences = request.Experiences,
            CvEnglishId = request.CvEnglishId,
            CvArabicId = request.CvArabicId,
            IsPublished = request.IsPublished,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.Teachers.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<TeacherDto?> UpdateAsync(TeacherDto dto)
    {
        var entity = await db.Teachers.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.Name = dto.Name;
        entity.PictureId = dto.PictureId;
        entity.Position = dto.Position;
        entity.Specialist = dto.Specialist;
        entity.ScientificDegree = dto.ScientificDegree;
        entity.AcademicDegree = dto.AcademicDegree;
        entity.Certificates = dto.Certificates;
        entity.Experiences = dto.Experiences;
        entity.CvEnglishId = dto.CvEnglishId;
        entity.CvArabicId = dto.CvArabicId;
        entity.IsPublished = dto.IsPublished;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.Teachers.FindAsync(id);
        if (entity is null) return false;

        db.Teachers.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static TeacherDto ToDto(Teacher t) => new()
    {
        Id = t.Id,
        Name = t.Name,
        PictureId = t.PictureId,
        Position = t.Position,
        Specialist = t.Specialist,
        ScientificDegree = t.ScientificDegree,
        AcademicDegree = t.AcademicDegree,
        Certificates = t.Certificates,
        Experiences = t.Experiences,
        CvEnglishId = t.CvEnglishId,
        CvArabicId = t.CvArabicId,
        IsPublished = t.IsPublished,
        DisplayOrder = t.DisplayOrder,
        IsActive = t.IsActive,
        CreatedAt = t.CreatedAt,
        UpdatedAt = t.UpdatedAt
    };
}
