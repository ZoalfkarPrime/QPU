using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class VacancyService(AppDBContext db) : IVacancyService
{
    public IQueryable<VacancyDto> GetQueryable() =>
        db.Vacancies.Select(v => new VacancyDto
        {
            Id = v.Id,
            Title = v.Title,
            Title_AR = v.Title_AR,
            Description = v.Description,
            Description_AR = v.Description_AR,
            IsActive = v.IsActive,
            DisplayOrder = v.DisplayOrder,
            CreatedAt = v.CreatedAt,
            UpdatedAt = v.UpdatedAt
        });

    public async Task<VacancyDto?> GetByIdAsync(int id) =>
        await GetQueryable().FirstOrDefaultAsync(v => v.Id == id);

    public async Task<VacancyDto> CreateAsync(CreateVacancyRequest request)
    {
        var entity = new Vacancy
        {
            Title = request.Title,
            Title_AR = request.Title_AR,
            Description = request.Description,
            Description_AR = request.Description_AR,
            IsActive = request.IsActive,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.Vacancies.Add(entity);
        await db.SaveChangesAsync();
        return await GetQueryable().FirstAsync(v => v.Id == entity.Id);
    }

    public async Task<VacancyDto?> UpdateAsync(VacancyDto dto)
    {
        var entity = await db.Vacancies.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.Title = dto.Title;
        entity.Title_AR = dto.Title_AR;
        entity.Description = dto.Description;
        entity.Description_AR = dto.Description_AR;
        entity.IsActive = dto.IsActive;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return await GetQueryable().FirstAsync(v => v.Id == entity.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.Vacancies.FindAsync(id);
        if (entity is null) return false;

        db.Vacancies.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
