using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class StudyProgramService(AppDBContext db) : IStudyProgramService
{
    public IQueryable<StudyProgramDto> GetQueryable() =>
        db.StudyPrograms.Select(sp => new StudyProgramDto
        {
            Id = sp.Id,
            StudyYearId = sp.StudyYearId,
            Name = sp.Name,
            Name_AR = sp.Name_AR,
            FileId = sp.FileId,
            File = sp.File == null ? null : new FileManagerNodeDto
            {
                Id = sp.File.Id,
                Name = sp.File.Name,
                Name_AR = sp.File.Name_AR,
                URL = sp.File.URL,
                Thumbnail = sp.File.Thumbnail,
                IsFile = sp.File.IsFile,
                FileType = sp.File.FileType
            },
            IsPublished = sp.IsPublished,
            DisplayOrder = sp.DisplayOrder,
            IsActive = sp.IsActive,
            CreatedAt = sp.CreatedAt,
            UpdatedAt = sp.UpdatedAt
        });

    public async Task<StudyProgramDto?> GetByIdAsync(int id)
    {
        return await GetQueryable().FirstOrDefaultAsync(sp => sp.Id == id);
    }

    public async Task<StudyProgramDto> CreateAsync(CreateStudyProgramRequest request)
    {
        var entity = new StudyProgram
        {
            StudyYearId = request.StudyYearId,
            Name = request.Name,
            Name_AR = request.Name_AR,
            FileId = request.FileId,
            IsPublished = request.IsPublished,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.StudyPrograms.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<StudyProgramDto?> UpdateAsync(StudyProgramDto dto)
    {
        var entity = await db.StudyPrograms.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.StudyYearId = dto.StudyYearId;
        entity.Name = dto.Name;
        entity.Name_AR = dto.Name_AR;
        entity.FileId = dto.FileId;
        entity.IsPublished = dto.IsPublished;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.StudyPrograms.FindAsync(id);
        if (entity is null) return false;

        db.StudyPrograms.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static StudyProgramDto ToDto(StudyProgram sp) => new()
    {
        Id = sp.Id,
        StudyYearId = sp.StudyYearId,
        Name = sp.Name,
        Name_AR = sp.Name_AR,
        FileId = sp.FileId,
        File = sp.File == null ? null : new FileManagerNodeDto
        {
            Id = sp.File.Id,
            Name = sp.File.Name,
            Name_AR = sp.File.Name_AR,
            URL = sp.File.URL,
            Thumbnail = sp.File.Thumbnail,
            IsFile = sp.File.IsFile,
            FileType = sp.File.FileType
        },
        IsPublished = sp.IsPublished,
        DisplayOrder = sp.DisplayOrder,
        IsActive = sp.IsActive,
        CreatedAt = sp.CreatedAt,
        UpdatedAt = sp.UpdatedAt
    };
}
