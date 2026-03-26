using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface IGraduatedStudentService
{
    IQueryable<GraduatedStudentDto> GetQueryable();
    Task<GraduatedStudentDto?> GetByIdAsync(int id);
    Task<GraduatedStudentDto> CreateAsync(CreateGraduatedStudentRequest request);
    Task<GraduatedStudentDto?> UpdateAsync(GraduatedStudentDto dto);
    Task<bool> DeleteAsync(int id);
}
