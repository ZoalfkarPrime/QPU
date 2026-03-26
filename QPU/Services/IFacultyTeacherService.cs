using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface IFacultyTeacherService
{
    IQueryable<FacultyTeacherDto> GetQueryable();
    Task<FacultyTeacherDto?> GetByIdAsync(int id);
    Task<FacultyTeacherDto> CreateAsync(CreateFacultyTeacherRequest request);
    Task<FacultyTeacherDto?> UpdateAsync(FacultyTeacherDto dto);
    Task<bool> DeleteAsync(int id);
}
