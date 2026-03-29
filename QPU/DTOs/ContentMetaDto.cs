using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class ContentMetaDto
{
    public int Id { get; set; }
    public int ContentId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Type_AR { get; set; }
    public string KeyName { get; set; } = string.Empty;
    public string? KeyName_AR { get; set; }
    public string? Value { get; set; }
    public string? Value_AR { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateContentMetaRequest
{
    [Required]
    public int ContentId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Type_AR { get; set; }

    [Required]
    [MaxLength(200)]
    public string KeyName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? KeyName_AR { get; set; }

    public string? Value { get; set; }
    public string? Value_AR { get; set; }

    public int DisplayOrder { get; set; }
}
