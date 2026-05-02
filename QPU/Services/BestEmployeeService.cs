using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class BestEmployeeService(AppDBContext db) : IBestEmployeeService
{
    public IQueryable<BestEmployeeDto> GetQueryable() =>
        db.BestEmployees.Select(be => new BestEmployeeDto
        {
            Id = be.Id,
            FacultyId = be.FacultyId,
            StudyYearId = be.StudyYearId,
            TeacherId = be.TeacherId,
            Description = be.Description,
            Description_AR = be.Description_AR,
            DisplayOrder = be.DisplayOrder,
            IsActive = be.IsActive,
            CreatedAt = be.CreatedAt,
            UpdatedAt = be.UpdatedAt
        });

    public async Task<BestEmployeeDto?> GetByIdAsync(int id)
    {
        return await GetQueryable().FirstOrDefaultAsync(be => be.Id == id);
    }

    public async Task<BestEmployeeDto> CreateAsync(CreateBestEmployeeRequest request)
    {
        var entity = new BestEmployee
        {
            FacultyId = request.FacultyId,
            StudyYearId = request.StudyYearId,
            TeacherId = request.TeacherId,
            Description = request.Description,
            Description_AR = request.Description_AR,
            DisplayOrder = request.DisplayOrder,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.BestEmployees.Add(entity);
        await db.SaveChangesAsync();
        return await GetQueryable().FirstAsync(be => be.Id == entity.Id);
    }

    public async Task<BestEmployeeDto?> UpdateAsync(BestEmployeeDto dto)
    {
        var entity = await db.BestEmployees.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.FacultyId = dto.FacultyId;
        entity.StudyYearId = dto.StudyYearId;
        entity.TeacherId = dto.TeacherId;
        entity.Description = dto.Description;
        entity.Description_AR = dto.Description_AR;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return await GetQueryable().FirstAsync(be => be.Id == entity.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.BestEmployees.FindAsync(id);
        if (entity is null) return false;

        db.BestEmployees.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
