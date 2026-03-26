using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class CourseTeacherDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateCourseTeacherRequest
{
    [Required]
    public int CourseId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    public int DisplayOrder { get; set; }
}
