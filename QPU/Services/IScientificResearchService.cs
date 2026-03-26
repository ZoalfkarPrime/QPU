using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface IScientificResearchService
{
    IQueryable<ScientificResearchDto> GetQueryable();
    Task<ScientificResearchDto?> GetByIdAsync(int id);
    Task<ScientificResearchDto> CreateAsync(CreateScientificResearchRequest request);
    Task<ScientificResearchDto?> UpdateAsync(ScientificResearchDto dto);
    Task<bool> DeleteAsync(int id);
}
