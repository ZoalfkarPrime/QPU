using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class FacultyDto
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Guid? PictureId { get; set; }
    public Guid? LogoId { get; set; }
    public bool Slider { get; set; }
    public bool IsPublished { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateFacultyRequest
{
    [Required]
    [MaxLength(150)]
    public string Slug { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public Guid? PictureId { get; set; }
    public Guid? LogoId { get; set; }
    public bool Slider { get; set; }
    public bool IsPublished { get; set; } = true;
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public int DisplayOrder { get; set; }
}

public class UpdateFacultyRequest
{
    [Required]
    [MaxLength(150)]
    public string Slug { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public Guid? PictureId { get; set; }
    public Guid? LogoId { get; set; }
    public bool Slider { get; set; }
    public bool IsPublished { get; set; }
    public string? PrimaryColor { get; set; }
    public string? SecondaryColor { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
}
