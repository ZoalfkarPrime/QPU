using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface ICourseTeacherService
{
    IQueryable<CourseTeacherDto> GetQueryable();
    Task<CourseTeacherDto?> GetByIdAsync(int id);
    Task<CourseTeacherDto> CreateAsync(CreateCourseTeacherRequest request);
    Task<CourseTeacherDto?> UpdateAsync(CourseTeacherDto dto);
    Task<bool> DeleteAsync(int id);
}
