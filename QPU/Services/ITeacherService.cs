using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface ITeacherService
{
    IQueryable<TeacherDto> GetQueryable();
    Task<TeacherDto?> GetByIdAsync(int id);
    Task<TeacherDto> CreateAsync(CreateTeacherRequest request);
    Task<TeacherDto?> UpdateAsync(TeacherDto dto);
    Task<bool> DeleteAsync(int id);
}
