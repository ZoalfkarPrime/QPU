using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class CourseDto
{
    public int Id { get; set; }
    public int FacultyId { get; set; }
    public int StudyYearId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsPublished { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateCourseRequest
{
    [Required]
    public int FacultyId { get; set; }

    [Required]
    public int StudyYearId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }
}
