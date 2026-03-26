using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface IStudyProgramService
{
    IQueryable<StudyProgramDto> GetQueryable();
    Task<StudyProgramDto?> GetByIdAsync(int id);
    Task<StudyProgramDto> CreateAsync(CreateStudyProgramRequest request);
    Task<StudyProgramDto?> UpdateAsync(StudyProgramDto dto);
    Task<bool> DeleteAsync(int id);
}
