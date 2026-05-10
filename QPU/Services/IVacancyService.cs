using QPU.DTOs;

namespace QPU.Services;

public interface IVacancyService
{
    IQueryable<VacancyDto> GetQueryable();
    Task<VacancyDto?> GetByIdAsync(int id);
    Task<VacancyDto> CreateAsync(CreateVacancyRequest request);
    Task<VacancyDto?> UpdateAsync(VacancyDto dto);
    Task<bool> DeleteAsync(int id);
}
