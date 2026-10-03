using System.ComponentModel.DataAnnotations;
using QPU_DataAccess.Models;

namespace QPU.DTOs;

public class StudentHighSchoolCertificateDto
{
    public int Id { get; set; }
    public int StudentRegistrationId { get; set; }

    [Required]
    public int CertificateTypeId { get; set; }
    public HighSchoolCertificateTypeLookupDto? CertificateType { get; set; }

    [MaxLength(200)]
    public string? CertificateSource { get; set; }

    [MaxLength(200)]
    public string? CertificatePlace { get; set; }

    public int? CertificateDate { get; set; }

    [MaxLength(50)]
    public string? CertificateOrSubscriptionNumber { get; set; }

    public int? ExamSessionId { get; set; }
    public ExamSessionLookupDto? ExamSession { get; set; }

    public decimal? GeneralTotal { get; set; }
    public decimal? Average { get; set; }
    public decimal? AdmissionAverageAfterLanguageExclusion { get; set; }
}

public class StudentRegistrationDto
{
    public int Id { get; set; }
    public string ApplicationNumber { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;
    public string? MotherName { get; set; }
    public string? BirthPlace { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string? NationalNumber { get; set; }
    public string? IdentityNumber { get; set; }
    public string? RegistrationPlace { get; set; }
    public string? RegistrationNumber { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Mobile { get; set; }

    public int FacultyId { get; set; }
    public FacultyLookupDto? Faculty { get; set; }

    public int AdmissionTypeId { get; set; }
    public AdmissionTypeLookupDto? AdmissionType { get; set; }

    public int OfficeId { get; set; }
    public OfficeLookupDto? Office { get; set; }

    public decimal AmountPaid { get; set; }
    public StudentRegistrationStatus Status { get; set; } = StudentRegistrationStatus.Draft;
    public string? Note { get; set; }

    public StudentHighSchoolCertificateDto? HighSchoolCertificate { get; set; }

    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateStudentHighSchoolCertificateRequest
{
    [Required]
    public int CertificateTypeId { get; set; }

    [MaxLength(200)]
    public string? CertificateSource { get; set; }

    [MaxLength(200)]
    public string? CertificatePlace { get; set; }

    public int? CertificateDate { get; set; }

    [MaxLength(50)]
    public string? CertificateOrSubscriptionNumber { get; set; }

    public int? ExamSessionId { get; set; }
    public decimal? GeneralTotal { get; set; }
    public decimal? Average { get; set; }
    public decimal? AdmissionAverageAfterLanguageExclusion { get; set; }
}

public class CreateStudentRegistrationRequest
{
    [Required]
    [MaxLength(300)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? MotherName { get; set; }

    [MaxLength(300)]
    public string? BirthPlace { get; set; }

    public DateOnly? BirthDate { get; set; }

    [MaxLength(50)]
    public string? NationalNumber { get; set; }

    [MaxLength(50)]
    public string? IdentityNumber { get; set; }

    [MaxLength(300)]
    public string? RegistrationPlace { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(50)]
    public string? Mobile { get; set; }

    [Required]
    public int FacultyId { get; set; }

    [Required]
    public int AdmissionTypeId { get; set; }

    [Required]
    public int OfficeId { get; set; }

    public decimal AmountPaid { get; set; }
    public StudentRegistrationStatus Status { get; set; } = StudentRegistrationStatus.Draft;
    public string? Note { get; set; }

    public CreateStudentHighSchoolCertificateRequest? HighSchoolCertificate { get; set; }
}
