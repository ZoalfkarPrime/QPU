using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface ILabService
{
    IQueryable<LabDto> GetQueryable();
    Task<LabDto?> GetByIdAsync(int id);
    Task<LabDto> CreateAsync(CreateLabRequest request);
    Task<LabDto?> UpdateAsync(LabDto dto);
    Task<bool> DeleteAsync(int id);
}
