using Kendo.Mvc.UI;
using QPU.DTOs;

namespace QPU.Services;

public interface ICourseService
{
    IQueryable<CourseDto> GetQueryable();
    Task<CourseDto?> GetByIdAsync(int id);
    Task<CourseDto> CreateAsync(CreateCourseRequest request);
    Task<CourseDto?> UpdateAsync(CourseDto dto);
    Task<bool> DeleteAsync(int id);
}
