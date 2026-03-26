using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface IFacultyService
{
    IQueryable<FacultyDto> GetQueryable();
    Task<FacultyDto?> GetByIdAsync(int id);
    Task<FacultyDto> CreateAsync(CreateFacultyRequest request);
    Task<FacultyDto?> UpdateAsync(FacultyDto dto);
    Task<bool> DeleteAsync(int id);
}
