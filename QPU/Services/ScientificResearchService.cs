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
            Details = r.Details,
            DownloadFileId = r.DownloadFileId,
            PublishedAt = r.PublishedAt,
            IsPublished = r.IsPublished,
            DisplayOrder = r.DisplayOrder,
            IsActive = r.IsActive,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        });

    public async Task<ScientificResearchDto?> GetByIdAsync(int id)
    {
        var entity = await db.ScientificResearches.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<ScientificResearchDto> CreateAsync(CreateScientificResearchRequest request)
    {
        var entity = new ScientificResearch
        {
            FacultyId = request.FacultyId,
            TeacherId = request.TeacherId,
            StudyYearId = request.StudyYearId,
            Title = request.Title,
            Details = request.Details,
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
        entity.Details = dto.Details;
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
        Details = r.Details,
        DownloadFileId = r.DownloadFileId,
        PublishedAt = r.PublishedAt,
        IsPublished = r.IsPublished,
        DisplayOrder = r.DisplayOrder,
        IsActive = r.IsActive,
        CreatedAt = r.CreatedAt,
        UpdatedAt = r.UpdatedAt
    };
}
