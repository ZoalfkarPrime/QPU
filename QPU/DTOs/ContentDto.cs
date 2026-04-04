using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class ContentDto
{
    public int Id { get; set; }
    public string ReferenceId { get; set; } = string.Empty;
    public string ReferenceType { get; set; } = string.Empty;
    public string Section { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public List<ContentMetaDto> ContentMetas { get; set; } = [];
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateContentRequest
{
    [Required]
    [MaxLength(100)]
    public string ReferenceId { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ReferenceType { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Section { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }
}
