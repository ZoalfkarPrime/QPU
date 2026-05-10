using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class VacancyDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Title_AR { get; set; }
    public string? Description { get; set; }
    public string? Description_AR { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateVacancyRequest
{
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Title_AR { get; set; }

    public string? Description { get; set; }
    public string? Description_AR { get; set; }
    public bool IsActive { get; set; } = true;
    public int DisplayOrder { get; set; }
}
