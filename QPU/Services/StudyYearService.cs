using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class StudyYearService(AppDBContext db) : IStudyYearService
{
    public IQueryable<StudyYearDto> GetQueryable() =>
        db.StudyYears.Select(s => new StudyYearDto
        {
            Id = s.Id,
            Name = s.Name,
            Name_AR = s.Name_AR,
            StartDate = s.StartDate,
            EndDate = s.EndDate,
            IsCurrent = s.IsCurrent,
            DisplayOrder = s.DisplayOrder,
            IsActive = s.IsActive,
            CreatedAt = s.CreatedAt,
            UpdatedAt = s.UpdatedAt
        });

    public async Task<StudyYearDto?> GetByIdAsync(int id)
    {
        var entity = await db.StudyYears.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<StudyYearDto> CreateAsync(CreateStudyYearRequest request)
    {
        var entity = new StudyYear
        {
            Name = request.Name,
            Name_AR = request.Name_AR,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IsCurrent = request.IsCurrent,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.StudyYears.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<StudyYearDto?> UpdateAsync(StudyYearDto dto)
    {
        var entity = await db.StudyYears.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.Name = dto.Name;
        entity.Name_AR = dto.Name_AR;
        entity.StartDate = dto.StartDate;
        entity.EndDate = dto.EndDate;
        entity.IsCurrent = dto.IsCurrent;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.StudyYears.FindAsync(id);
        if (entity is null) return false;

        db.StudyYears.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static StudyYearDto ToDto(StudyYear s) => new()
    {
        Id = s.Id,
        Name = s.Name,
        Name_AR = s.Name_AR,
        StartDate = s.StartDate,
        EndDate = s.EndDate,
        IsCurrent = s.IsCurrent,
        DisplayOrder = s.DisplayOrder,
        IsActive = s.IsActive,
        CreatedAt = s.CreatedAt,
        UpdatedAt = s.UpdatedAt
    };
}
