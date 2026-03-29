using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class LabDto
{
    public int Id { get; set; }
    public int FacultyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Name_AR { get; set; }
    public Guid? PictureId { get; set; }
    public string? Content { get; set; }
    public string? Content_AR { get; set; }
    public bool IsPublished { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateLabRequest
{
    [Required]
    public int FacultyId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Name_AR { get; set; }

    public Guid? PictureId { get; set; }
    public string? Content { get; set; }
    public string? Content_AR { get; set; }
    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }
}
