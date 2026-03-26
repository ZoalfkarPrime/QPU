using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class GraduatedStudentDto
{
    public int Id { get; set; }
    public int StudyYearId { get; set; }
    public int FacultyId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public decimal Average { get; set; }
    public string? StudentNumber { get; set; }
    public bool IsPublished { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateGraduatedStudentRequest
{
    [Required]
    public int StudyYearId { get; set; }

    [Required]
    public int FacultyId { get; set; }

    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    public decimal Average { get; set; }

    [MaxLength(20)]
    public string? StudentNumber { get; set; }

    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }
}
