using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class CourseTeacherService(AppDBContext db) : ICourseTeacherService
{
    public IQueryable<CourseTeacherDto> GetQueryable() =>
        db.CourseTeachers.OrderBy(ct => ct.DisplayOrder).Select(ct => new CourseTeacherDto
        {
            Id = ct.Id,
            CourseId = ct.CourseId,
            Course = ct.Course == null ? null : new CourseLookupDto
            {
                Id = ct.Course.Id,
                Name = ct.Course.Name,
                Name_AR = ct.Course.Name_AR,
                FacultyId = ct.Course.FacultyId,
                StudyYearId = ct.Course.StudyYearId
            },
            TeacherId = ct.TeacherId,
            Teacher = ct.Teacher == null ? null : new TeacherLookupDto
            {
                Id = ct.Teacher.Id,
                Name = ct.Teacher.Name,
                Name_AR = ct.Teacher.Name_AR,
                Picture = ct.Teacher.Picture == null ? null : new FileManagerNodeDto
                {
                    Id = ct.Teacher.Picture.Id,
                    Name = ct.Teacher.Picture.Name,
                    Name_AR = ct.Teacher.Picture.Name_AR,
                    URL = ct.Teacher.Picture.URL,
                    Thumbnail = ct.Teacher.Picture.Thumbnail,
                    IsFile = ct.Teacher.Picture.IsFile,
                    FileType = ct.Teacher.Picture.FileType
                }
            },
            DisplayOrder = ct.DisplayOrder,
            IsActive = ct.IsActive,
            CreatedAt = ct.CreatedAt,
            UpdatedAt = ct.UpdatedAt
        });

    public async Task<CourseTeacherDto?> GetByIdAsync(int id)
    {
        var entity = await db.CourseTeachers.AsNoTracking().FirstOrDefaultAsync(ct => ct.Id == id);
        return entity is null ? null : ToDto(entity);
    }

    public async Task<CourseTeacherDto> CreateAsync(CreateCourseTeacherRequest request)
    {
        var entity = new CourseTeacher
        {
            CourseId = request.CourseId,
            TeacherId = request.TeacherId,
            DisplayOrder = request.DisplayOrder,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        db.CourseTeachers.Add(entity);
        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<CourseTeacherDto?> UpdateAsync(CourseTeacherDto dto)
    {
        var entity = await db.CourseTeachers.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.CourseId = dto.CourseId;
        entity.TeacherId = dto.TeacherId;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return ToDto(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.CourseTeachers.FindAsync(id);
        if (entity is null) return false;

        db.CourseTeachers.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    private static CourseTeacherDto ToDto(CourseTeacher ct) => new()
    {
        Id = ct.Id,
        CourseId = ct.CourseId,
        TeacherId = ct.TeacherId,
        DisplayOrder = ct.DisplayOrder,
        IsActive = ct.IsActive,
        CreatedAt = ct.CreatedAt,
        UpdatedAt = ct.UpdatedAt
    };
}
