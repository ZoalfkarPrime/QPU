using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class TeacherDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid? PictureId { get; set; }
    public string? Position { get; set; }
    public string? Specialist { get; set; }
    public string? ScientificDegree { get; set; }
    public string? AcademicDegree { get; set; }
    public string? Certificates { get; set; }
    public string? Experiences { get; set; }
    public Guid? CvEnglishId { get; set; }
    public Guid? CvArabicId { get; set; }
    public bool IsPublished { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateTeacherRequest
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public Guid? PictureId { get; set; }

    [MaxLength(200)]
    public string? Position { get; set; }

    [MaxLength(300)]
    public string? Specialist { get; set; }

    [MaxLength(300)]
    public string? ScientificDegree { get; set; }

    [MaxLength(300)]
    public string? AcademicDegree { get; set; }

    public string? Certificates { get; set; }
    public string? Experiences { get; set; }
    public Guid? CvEnglishId { get; set; }
    public Guid? CvArabicId { get; set; }
    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }
}
