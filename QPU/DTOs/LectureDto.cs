using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class LectureDto
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int TeacherId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Content { get; set; }
    public Guid? FileId { get; set; }
    public int LectureNumber { get; set; }
    public bool IsPublished { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateLectureRequest
{
    [Required]
    public int CourseId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    public string? Content { get; set; }
    public Guid? FileId { get; set; }
    public int LectureNumber { get; set; }
    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }
}
