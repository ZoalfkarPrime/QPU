using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class StudyProgramDto
{
    public int Id { get; set; }
    public int StudyYearId { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? FileId { get; set; }
    public bool IsPublished { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateStudyProgramRequest
{
    [Required]
    public int StudyYearId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Name { get; set; } = string.Empty;

    public Guid? FileId { get; set; }
    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }
}
