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
            Name_AR = t.Name_AR,
            PictureId = t.PictureId,
            Picture = t.Picture == null ? null : new FileManagerNodeDto
            {
                Id = t.Picture.Id,
                Name = t.Picture.Name,
                Name_AR = t.Picture.Name_AR,
                URL = t.Picture.URL,
                Thumbnail = t.Picture.Thumbnail,
                IsFile = t.Picture.IsFile,
                FileType = t.Picture.FileType
            },
            Position = t.Position,
            Position_AR = t.Position_AR,
            Specialist = t.Specialist,
            Specialist_AR = t.Specialist_AR,
            ScientificDegree = t.ScientificDegree,
            ScientificDegree_AR = t.ScientificDegree_AR,
            AcademicDegree = t.AcademicDegree,
            AcademicDegree_AR = t.AcademicDegree_AR,
            Certificates = t.Certificates,
            Certificates_AR = t.Certificates_AR,
            Experiences = t.Experiences,
            Experiences_AR = t.Experiences_AR,
            CvEnglishId = t.CvEnglishId,
            CvEnglish = t.CvEnglish == null ? null : new FileManagerNodeDto
            {
                Id = t.CvEnglish.Id,
                Name = t.CvEnglish.Name,
                Name_AR = t.CvEnglish.Name_AR,
                URL = t.CvEnglish.URL,
                Thumbnail = t.CvEnglish.Thumbnail,
                IsFile = t.CvEnglish.IsFile,
                FileType = t.CvEnglish.FileType
            },
            CvArabicId = t.CvArabicId,
            CvArabic = t.CvArabic == null ? null : new FileManagerNodeDto
            {
                Id = t.CvArabic.Id,
                Name = t.CvArabic.Name,
                Name_AR = t.CvArabic.Name_AR,
                URL = t.CvArabic.URL,
                Thumbnail = t.CvArabic.Thumbnail,
                IsFile = t.CvArabic.IsFile,
                FileType = t.CvArabic.FileType
            },
            IsPublished = t.IsPublished,
            DisplayOrder = t.DisplayOrder,
            IsActive = t.IsActive,
            CreatedAt = t.CreatedAt,
            UpdatedAt = t.UpdatedAt
        });

    public async Task<TeacherDto?> GetByIdAsync(int id)
    {
        return await GetQueryable().FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TeacherDto> CreateAsync(CreateTeacherRequest request)
    {
        var entity = new Teacher
        {
            Name = request.Name,
            Name_AR = request.Name_AR,
            PictureId = request.PictureId,
            Position = request.Position,
            Position_AR = request.Position_AR,
            Specialist = request.Specialist,
            Specialist_AR = request.Specialist_AR,
            ScientificDegree = request.ScientificDegree,
            ScientificDegree_AR = request.ScientificDegree_AR,
            AcademicDegree = request.AcademicDegree,
            AcademicDegree_AR = request.AcademicDegree_AR,
            Certificates = request.Certificates,
            Certificates_AR = request.Certificates_AR,
            Experiences = request.Experiences,
            Experiences_AR = request.Experiences_AR,
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
        entity.Name_AR = dto.Name_AR;
        entity.PictureId = dto.PictureId;
        entity.Position = dto.Position;
        entity.Position_AR = dto.Position_AR;
        entity.Specialist = dto.Specialist;
        entity.Specialist_AR = dto.Specialist_AR;
        entity.ScientificDegree = dto.ScientificDegree;
        entity.ScientificDegree_AR = dto.ScientificDegree_AR;
        entity.AcademicDegree = dto.AcademicDegree;
        entity.AcademicDegree_AR = dto.AcademicDegree_AR;
        entity.Certificates = dto.Certificates;
        entity.Certificates_AR = dto.Certificates_AR;
        entity.Experiences = dto.Experiences;
        entity.Experiences_AR = dto.Experiences_AR;
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
        Name_AR = t.Name_AR,
        PictureId = t.PictureId,
        Picture = t.Picture == null ? null : new FileManagerNodeDto
        {
            Id = t.Picture.Id,
            Name = t.Picture.Name,
            Name_AR = t.Picture.Name_AR,
            URL = t.Picture.URL,
            Thumbnail = t.Picture.Thumbnail,
            IsFile = t.Picture.IsFile,
            FileType = t.Picture.FileType
        },
        Position = t.Position,
        Position_AR = t.Position_AR,
        Specialist = t.Specialist,
        Specialist_AR = t.Specialist_AR,
        ScientificDegree = t.ScientificDegree,
        ScientificDegree_AR = t.ScientificDegree_AR,
        AcademicDegree = t.AcademicDegree,
        AcademicDegree_AR = t.AcademicDegree_AR,
        Certificates = t.Certificates,
        Certificates_AR = t.Certificates_AR,
        Experiences = t.Experiences,
        Experiences_AR = t.Experiences_AR,
        CvEnglishId = t.CvEnglishId,
        CvEnglish = t.CvEnglish == null ? null : new FileManagerNodeDto
        {
            Id = t.CvEnglish.Id,
            Name = t.CvEnglish.Name,
            Name_AR = t.CvEnglish.Name_AR,
            URL = t.CvEnglish.URL,
            Thumbnail = t.CvEnglish.Thumbnail,
            IsFile = t.CvEnglish.IsFile,
            FileType = t.CvEnglish.FileType
        },
        CvArabicId = t.CvArabicId,
        CvArabic = t.CvArabic == null ? null : new FileManagerNodeDto
        {
            Id = t.CvArabic.Id,
            Name = t.CvArabic.Name,
            Name_AR = t.CvArabic.Name_AR,
            URL = t.CvArabic.URL,
            Thumbnail = t.CvArabic.Thumbnail,
            IsFile = t.CvArabic.IsFile,
            FileType = t.CvArabic.FileType
        },
        IsPublished = t.IsPublished,
        DisplayOrder = t.DisplayOrder,
        IsActive = t.IsActive,
        CreatedAt = t.CreatedAt,
        UpdatedAt = t.UpdatedAt
    };
}
