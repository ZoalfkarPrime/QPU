using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class ContentDto
{
    public int Id { get; set; }
    public int ReferenceId { get; set; }
    public string ReferenceType { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
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

    [Required]
    [MaxLength(100)]
    public string Type { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
}
