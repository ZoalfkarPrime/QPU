using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class TeacherDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Name_AR { get; set; }
    public Guid? PictureId { get; set; }
    public FileManagerNodeDto? Picture { get; set; }
    public string? Position { get; set; }
    public string? Position_AR { get; set; }
    public string? Specialist { get; set; }
    public string? Specialist_AR { get; set; }
    public string? ScientificDegree { get; set; }
    public string? ScientificDegree_AR { get; set; }
    public string? AcademicDegree { get; set; }
    public string? AcademicDegree_AR { get; set; }
    public string? Certificates { get; set; }
    public string? Certificates_AR { get; set; }
    public string? Experiences { get; set; }
    public string? Experiences_AR { get; set; }
    public Guid? CvEnglishId { get; set; }
    public FileManagerNodeDto? CvEnglish { get; set; }
    public Guid? CvArabicId { get; set; }
    public FileManagerNodeDto? CvArabic { get; set; }
    public bool IsPublished { get; set; }
    public bool? HasHonor { get; set; }
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

    [MaxLength(200)]
    public string? Name_AR { get; set; }

    public Guid? PictureId { get; set; }

    [MaxLength(200)]
    public string? Position { get; set; }

    [MaxLength(200)]
    public string? Position_AR { get; set; }

    [MaxLength(300)]
    public string? Specialist { get; set; }

    [MaxLength(300)]
    public string? Specialist_AR { get; set; }

    [MaxLength(300)]
    public string? ScientificDegree { get; set; }

    [MaxLength(300)]
    public string? ScientificDegree_AR { get; set; }

    [MaxLength(300)]
    public string? AcademicDegree { get; set; }

    [MaxLength(300)]
    public string? AcademicDegree_AR { get; set; }

    public string? Certificates { get; set; }
    public string? Certificates_AR { get; set; }
    public string? Experiences { get; set; }
    public string? Experiences_AR { get; set; }
    public Guid? CvEnglishId { get; set; }
    public Guid? CvArabicId { get; set; }
    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }
}
