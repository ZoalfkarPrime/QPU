using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class ScientificResearchDto
{
    public int Id { get; set; }
    public int FacultyId { get; set; }
    public int TeacherId { get; set; }
    public int? StudyYearId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Title_AR { get; set; }
    public string? Details { get; set; }
    public string? Details_AR { get; set; }
    public Guid? DownloadFileId { get; set; }
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateScientificResearchRequest
{
    [Required]
    public int FacultyId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    public int? StudyYearId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Title_AR { get; set; }

    public string? Details { get; set; }
    public string? Details_AR { get; set; }
    public Guid? DownloadFileId { get; set; }
    public DateTime? PublishedAt { get; set; }
    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }
}
