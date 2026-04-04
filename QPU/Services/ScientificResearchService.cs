using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class ScientificResearchService(AppDBContext db) : IScientificResearchService
{
    public IQueryable<ScientificResearchDto> GetQueryable() =>
        db.ScientificResearches.Select(r => new ScientificResearchDto
        {
            Id = r.Id,
            FacultyId = r.FacultyId,
            TeacherId = r.TeacherId,
            StudyYearId = r.StudyYearId,
            Title = r.Title,
            Title_AR = r.Title_AR,
            Details = r.Details,
            Details_AR = r.Details_AR,
            DownloadFileId = r.DownloadFileId,
            DownloadFile = r.DownloadFile == null ? null : new FileManagerNodeDto
            {
                Id = r.DownloadFile.Id,
                Name = r.DownloadFile.Name,
                Name_AR = r.DownloadFile.Name_AR,
                URL = r.DownloadFile.URL,
                Thumbnail = r.DownloadFile.Thumbnail,
                IsFile = r.DownloadFile.IsFile,
                FileType = r.DownloadFile.FileType
            },
            PublishedAt = r.PublishedAt,
            IsPublished = r.IsPublished,
            DisplayOrder = r.DisplayOrder,
            IsActive = r.IsActive,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        });

    public async Task<ScientificResearchDto?> GetByIdAsync(int id)
    {
        return await GetQueryable().FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<ScientificResearchDto> CreateAsync(CreateScientificResearchRequest request)
    {
        var entity = new ScientificResearch
        {
            FacultyId = request.FacultyId,
            TeacherId = request.TeacherId,
            StudyYearId = request.StudyYearId,
            Title = request.Title,
            Title_AR = request.Title_AR,
            Details = request.Details,
            Details_AR = request.Details_AR,
            DownloadFileId = request.DownloadFileId,
            PublishedAt = request.PublishedAt,
            IsPublished = request.IsPublished,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.ScientificResearches.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<ScientificResearchDto?> UpdateAsync(ScientificResearchDto dto)
    {
        var entity = await db.ScientificResearches.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.FacultyId = dto.FacultyId;
        entity.TeacherId = dto.TeacherId;
        entity.StudyYearId = dto.StudyYearId;
        entity.Title = dto.Title;
        entity.Title_AR = dto.Title_AR;
        entity.Details = dto.Details;
        entity.Details_AR = dto.Details_AR;
        entity.DownloadFileId = dto.DownloadFileId;
        entity.PublishedAt = dto.PublishedAt;
        entity.IsPublished = dto.IsPublished;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.ScientificResearches.FindAsync(id);
        if (entity is null) return false;

        db.ScientificResearches.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static ScientificResearchDto ToDto(ScientificResearch r) => new()
    {
        Id = r.Id,
        FacultyId = r.FacultyId,
        TeacherId = r.TeacherId,
        StudyYearId = r.StudyYearId,
        Title = r.Title,
        Title_AR = r.Title_AR,
        Details = r.Details,
        Details_AR = r.Details_AR,
        DownloadFileId = r.DownloadFileId,
        DownloadFile = r.DownloadFile == null ? null : new FileManagerNodeDto
        {
            Id = r.DownloadFile.Id,
            Name = r.DownloadFile.Name,
            Name_AR = r.DownloadFile.Name_AR,
            URL = r.DownloadFile.URL,
            Thumbnail = r.DownloadFile.Thumbnail,
            IsFile = r.DownloadFile.IsFile,
            FileType = r.DownloadFile.FileType
        },
        PublishedAt = r.PublishedAt,
        IsPublished = r.IsPublished,
        DisplayOrder = r.DisplayOrder,
        IsActive = r.IsActive,
        CreatedAt = r.CreatedAt,
        UpdatedAt = r.UpdatedAt
    };
}
