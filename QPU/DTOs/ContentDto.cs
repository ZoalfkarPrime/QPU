using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class ContentDto
{
    public int Id { get; set; }
    public int ReferenceId { get; set; }
    public string ReferenceType { get; set; } = string.Empty;
    public string? ReferenceType_AR { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Type_AR { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Title_AR { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateContentRequest
{
    [Required]
    public int ReferenceId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ReferenceType { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? ReferenceType_AR { get; set; }

    [Required]
    [MaxLength(100)]
    public string Type { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Type_AR { get; set; }

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Title_AR { get; set; }

    public int DisplayOrder { get; set; }
}
