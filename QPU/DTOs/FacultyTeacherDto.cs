using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class FacultyTeacherDto
{
    public int Id { get; set; }
    public int FacultyId { get; set; }
    public int TeacherId { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateFacultyTeacherRequest
{
    [Required]
    public int FacultyId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    public int DisplayOrder { get; set; }
}
