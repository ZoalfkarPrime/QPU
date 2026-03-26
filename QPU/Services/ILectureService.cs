using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface ILectureService
{
    IQueryable<LectureDto> GetQueryable();
    Task<LectureDto?> GetByIdAsync(int id);
    Task<LectureDto> CreateAsync(CreateLectureRequest request);
    Task<LectureDto?> UpdateAsync(LectureDto dto);
    Task<bool> DeleteAsync(int id);
}
