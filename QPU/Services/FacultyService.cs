using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class FacultyService(AppDBContext db) : IFacultyService
{
    public IQueryable<FacultyDto> GetQueryable() =>
        db.Faculties.Select(f => new FacultyDto
        {
            Id = f.Id,
            Slug = f.Slug,
            Name = f.Name,
            Name_AR = f.Name_AR,
            PictureId = f.PictureId,
            Picture = f.Picture == null ? null : new FileManagerNodeDto
            {
                Id = f.Picture.Id,
                Name = f.Picture.Name,
                Name_AR = f.Picture.Name_AR,
                URL = f.Picture.URL,
                Thumbnail = f.Picture.Thumbnail,
                IsFile = f.Picture.IsFile,
                FileType = f.Picture.FileType
            },
            LogoId = f.LogoId,
            Logo = f.Logo == null ? null : new FileManagerNodeDto
            {
                Id = f.Logo.Id,
                Name = f.Logo.Name,
                Name_AR = f.Logo.Name_AR,
                URL = f.Logo.URL,
                Thumbnail = f.Logo.Thumbnail,
                IsFile = f.Logo.IsFile,
                FileType = f.Logo.FileType
            },
            Slider = f.Slider,
            IsPublished = f.IsPublished,
            PrimaryColor = f.PrimaryColor,
            SecondaryColor = f.SecondaryColor,
            DisplayOrder = f.DisplayOrder,
            IsActive = f.IsActive,
            CreatedAt = f.CreatedAt,
            UpdatedAt = f.UpdatedAt
        });

    public async Task<FacultyDto?> GetByIdAsync(int id)
    {
        return await GetQueryable().FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<FacultyDto> CreateAsync(CreateFacultyRequest request)
    {
        var faculty = new Faculty
        {
            Slug = request.Slug,
            Name = request.Name,
            Name_AR = request.Name_AR,
            PictureId = request.PictureId,
            LogoId = request.LogoId,
            Slider = request.Slider,
            IsPublished = request.IsPublished,
            PrimaryColor = request.PrimaryColor,
            SecondaryColor = request.SecondaryColor,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.Faculties.Add(faculty);
        await db.SaveChangesAsync();

        return ToDto(faculty);
    }

    public async Task<FacultyDto?> UpdateAsync(FacultyDto dto)
    {
        var faculty = await db.Faculties.FindAsync(dto.Id);

        if (faculty is null)
            return null;

        faculty.Slug = dto.Slug;
        faculty.Name = dto.Name;
        faculty.Name_AR = dto.Name_AR;
        faculty.PictureId = dto.PictureId;
        faculty.LogoId = dto.LogoId;
        faculty.Slider = dto.Slider;
        faculty.IsPublished = dto.IsPublished;
        faculty.PrimaryColor = dto.PrimaryColor;
        faculty.SecondaryColor = dto.SecondaryColor;
        faculty.DisplayOrder = dto.DisplayOrder;
        faculty.IsActive = dto.IsActive;
        faculty.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();

        return ToDto(faculty);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var faculty = await db.Faculties.FindAsync(id);

        if (faculty is null)
            return false;

        db.Faculties.Remove(faculty);
        await db.SaveChangesAsync();

        return true;
    }

    private static FacultyDto ToDto(Faculty f) => new()
    {
        Id = f.Id,
        Slug = f.Slug,
        Name = f.Name,
        Name_AR = f.Name_AR,
        PictureId = f.PictureId,
        Picture = f.Picture == null ? null : new FileManagerNodeDto
        {
            Id = f.Picture.Id,
            Name = f.Picture.Name,
            Name_AR = f.Picture.Name_AR,
            URL = f.Picture.URL,
            Thumbnail = f.Picture.Thumbnail,
            IsFile = f.Picture.IsFile,
            FileType = f.Picture.FileType
        },
        LogoId = f.LogoId,
        Logo = f.Logo == null ? null : new FileManagerNodeDto
        {
            Id = f.Logo.Id,
            Name = f.Logo.Name,
            Name_AR = f.Logo.Name_AR,
            URL = f.Logo.URL,
            Thumbnail = f.Logo.Thumbnail,
            IsFile = f.Logo.IsFile,
            FileType = f.Logo.FileType
        },
        Slider = f.Slider,
        IsPublished = f.IsPublished,
        PrimaryColor = f.PrimaryColor,
        SecondaryColor = f.SecondaryColor,
        DisplayOrder = f.DisplayOrder,
        IsActive = f.IsActive,
        CreatedAt = f.CreatedAt,
        UpdatedAt = f.UpdatedAt
    };
}
