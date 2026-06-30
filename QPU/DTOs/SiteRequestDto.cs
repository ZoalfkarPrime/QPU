using System.ComponentModel.DataAnnotations;
using QPU_DataAccess.Models;

namespace QPU.DTOs;

public class SiteRequestDto
{
    public int Id { get; set; }
    public RequestCategory Category { get; set; }

    // Employment — vacancy
    public int? VacancyId { get; set; }
    public VacancyDto? Vacancy { get; set; }

    // Shared
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    // Employment only
    public DateOnly? DateOfBirth { get; set; }
    public string? PlaceOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public string? Nationality { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public Guid? CvFileId { get; set; }
    public FileManagerNodeDto? CvFile { get; set; }
    public Guid? DegreeFileId { get; set; }
    public FileManagerNodeDto? DegreeFile { get; set; }

    // ContactUs only
    public string? MessageTitle { get; set; }
    public string? MessageBody { get; set; }

    public int? ContractFacultyId { get; set; }
    public FacultyDto? ContractFaculty { get; set; }
    public string? ContractScientificDegree { get; set; }
    public string? ContractSpecialist { get; set; }
    public string? ContractJob { get; set; }
    public bool? HasContractScientificDegreeApproved { get; set; }
    public bool? HasContractExperience { get; set; }
    public string? ContractExperiences { get; set; }
    public string? ContractLanguages { get; set; }
    public string? ContractCurrentPlace { get; set; }
    public bool? ContractFulltimeJob { get; set; }
    public bool? HasContractAnotherJob { get; set; }

    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

// ── Employment request ───────────────────────────────────────────────────────

public class CreateEmploymentRequest
{
    [Required]
    public int VacancyId { get; set; }

    [Required]
    [MaxLength(200)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string LastName { get; set; } = string.Empty;

    public DateOnly? DateOfBirth { get; set; }

    [MaxLength(300)]
    public string? PlaceOfBirth { get; set; }

    public Gender? Gender { get; set; }

    [MaxLength(100)]
    public string? Nationality { get; set; }

    [MaxLength(50)]
    public string? PhoneNumber { get; set; }

    [MaxLength(200)]
    public string? Email { get; set; }

    public MaritalStatus? MaritalStatus { get; set; }

    public int? ContractFacultyId { get; set; }
    public string? ContractScientificDegree { get; set; }
    public string? ContractSpecialist { get; set; }
    public string? ContractJob { get; set; }
    public bool? HasContractScientificDegreeApproved { get; set; }
    public bool? HasContractExperience { get; set; }
    public string? ContractExperiences { get; set; }
    public string? ContractLanguages { get; set; }
    public string? ContractCurrentPlace { get; set; }
    public bool? ContractFulltimeJob { get; set; }
    public bool? HasContractAnotherJob { get; set; }

    // Optional CV file — sent as multipart/form-data
    public IFormFile? CvFile { get; set; }
    public IFormFile? DegreeFile { get; set; }
}

// ── Contact-us request ───────────────────────────────────────────────────────

public class CreateContactUsRequest
{
    [Required]
    [MaxLength(200)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? PhoneNumber { get; set; }

    [Required]
    [MaxLength(300)]
    public string MessageTitle { get; set; } = string.Empty;

    public string? MessageBody { get; set; }
}
