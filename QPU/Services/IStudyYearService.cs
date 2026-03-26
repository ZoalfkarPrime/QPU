using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface IStudyYearService
{
    IQueryable<StudyYearDto> GetQueryable();
    Task<StudyYearDto?> GetByIdAsync(int id);
    Task<StudyYearDto> CreateAsync(CreateStudyYearRequest request);
    Task<StudyYearDto?> UpdateAsync(StudyYearDto dto);
    Task<bool> DeleteAsync(int id);
}
