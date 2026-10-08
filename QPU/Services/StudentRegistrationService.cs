using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class StudentRegistrationService(AppDBContext db) : IStudentRegistrationService
{
    public IQueryable<StudentRegistrationDto> GetQueryable() =>
        db.StudentRegistrations
            .Include(r => r.Faculty)
            .Include(r => r.AdmissionType)
            .Include(r => r.Office)
            .Include(r => r.HighSchoolCertificate).ThenInclude(c => c!.CertificateType)
            .Include(r => r.HighSchoolCertificate).ThenInclude(c => c!.ExamSession)
            .OrderByDescending(r => r.CreatedAt)
            .Select(r => ToDtoProjection(r));

    public async Task<StudentRegistrationDto?> GetByIdAsync(int id)
    {
        var entity = await db.StudentRegistrations
            .AsNoTracking()
            .Include(r => r.Faculty)
            .Include(r => r.AdmissionType)
            .Include(r => r.Office)
            .Include(r => r.HighSchoolCertificate).ThenInclude(c => c!.CertificateType)
            .Include(r => r.HighSchoolCertificate).ThenInclude(c => c!.ExamSession)
            .FirstOrDefaultAsync(r => r.Id == id);

        return entity is null ? null : ToDto(entity);
    }

    public async Task<StudentRegistrationDto> CreateAsync(CreateStudentRegistrationRequest request)
    {
        var entity = new StudentRegistration
        {
            ApplicationNumber = await GenerateApplicationNumberAsync(),
            FullName = request.FullName,
            MotherName = request.MotherName,
            BirthPlace = request.BirthPlace,
            BirthDate = request.BirthDate,
            NationalNumber = request.NationalNumber,
            IdentityNumber = request.IdentityNumber,
            RegistrationPlace = request.RegistrationPlace,
            RegistrationNumber = await GenerateRegistrationNumberAsync(request.FacultyId),
            Address = request.Address,
            Phone = request.Phone,
            Mobile = request.Mobile,
            FacultyId = request.FacultyId,
            AdmissionTypeId = request.AdmissionTypeId,
            OfficeId = request.OfficeId,
            AmountPaid = request.AmountPaid,
            Status = request.Status,
            Note = request.Note,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        if (request.HighSchoolCertificate is not null)
        {
            entity.HighSchoolCertificate = new StudentHighSchoolCertificate
            {
                CertificateTypeId = request.HighSchoolCertificate.CertificateTypeId,
                CertificateSource = request.HighSchoolCertificate.CertificateSource,
                CertificatePlace = request.HighSchoolCertificate.CertificatePlace,
                CertificateDate = request.HighSchoolCertificate.CertificateDate,
                CertificateOrSubscriptionNumber = request.HighSchoolCertificate.CertificateOrSubscriptionNumber,
                ExamSessionId = request.HighSchoolCertificate.ExamSessionId,
                GeneralTotal = request.HighSchoolCertificate.GeneralTotal,
                Average = request.HighSchoolCertificate.Average,
                AdmissionAverageAfterLanguageExclusion = request.HighSchoolCertificate.AdmissionAverageAfterLanguageExclusion,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true
            };
        }

        db.StudentRegistrations.Add(entity);
        await db.SaveChangesAsync();

        return await GetByIdAsync(entity.Id) ?? ToDto(entity);
    }

    public async Task<StudentRegistrationDto?> UpdateAsync(StudentRegistrationDto dto)
    {
        var entity = await db.StudentRegistrations
            .Include(r => r.HighSchoolCertificate)
            .FirstOrDefaultAsync(r => r.Id == dto.Id);

        if (entity is null) return null;

        entity.FullName = dto.FullName;
        entity.MotherName = dto.MotherName;
        entity.BirthPlace = dto.BirthPlace;
        entity.BirthDate = dto.BirthDate;
        entity.NationalNumber = dto.NationalNumber;
        entity.IdentityNumber = dto.IdentityNumber;
        entity.RegistrationPlace = dto.RegistrationPlace;
        entity.RegistrationNumber = dto.RegistrationNumber;
        entity.Address = dto.Address;
        entity.Phone = dto.Phone;
        entity.Mobile = dto.Mobile;
        entity.FacultyId = dto.FacultyId;
        entity.AdmissionTypeId = dto.AdmissionTypeId;
        entity.OfficeId = dto.OfficeId;
        entity.AmountPaid = dto.AmountPaid;
        entity.Status = dto.Status;
        entity.Note = dto.Note;
        entity.IsActive = dto.IsActive;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.UpdatedAt = DateTime.UtcNow;

        if (dto.HighSchoolCertificate is not null)
        {
            if (entity.HighSchoolCertificate is null)
            {
                entity.HighSchoolCertificate = new StudentHighSchoolCertificate
                {
                    StudentRegistrationId = entity.Id,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
            }

            entity.HighSchoolCertificate.CertificateTypeId = dto.HighSchoolCertificate.CertificateTypeId;
            entity.HighSchoolCertificate.CertificateSource = dto.HighSchoolCertificate.CertificateSource;
            entity.HighSchoolCertificate.CertificatePlace = dto.HighSchoolCertificate.CertificatePlace;
            entity.HighSchoolCertificate.CertificateDate = dto.HighSchoolCertificate.CertificateDate;
            entity.HighSchoolCertificate.CertificateOrSubscriptionNumber = dto.HighSchoolCertificate.CertificateOrSubscriptionNumber;
            entity.HighSchoolCertificate.ExamSessionId = dto.HighSchoolCertificate.ExamSessionId;
            entity.HighSchoolCertificate.GeneralTotal = dto.HighSchoolCertificate.GeneralTotal;
            entity.HighSchoolCertificate.Average = dto.HighSchoolCertificate.Average;
            entity.HighSchoolCertificate.AdmissionAverageAfterLanguageExclusion = dto.HighSchoolCertificate.AdmissionAverageAfterLanguageExclusion;
            entity.HighSchoolCertificate.UpdatedAt = DateTime.UtcNow;
        }

        await db.SaveChangesAsync();
        return await GetByIdAsync(entity.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.StudentRegistrations.FindAsync(id);
        if (entity is null) return false;

        db.StudentRegistrations.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }

    public async Task<StudentRegistrationDto?> AdvanceStatusAsync(int id)
    {
        var entity = await db.StudentRegistrations.FindAsync(id);
        if (entity is null) return null;

        var next = GetNextStatus(entity.Status) ??
            throw new InvalidOperationException($"Registration is in a terminal or unrecognized status ({entity.Status}); it cannot be advanced.");

        entity.Status = next;
        entity.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();

        return await GetByIdAsync(entity.Id);
    }

    public async Task<StudentRegistrationDto?> CancelAsync(int id)
    {
        var entity = await db.StudentRegistrations.FindAsync(id);
        if (entity is null) return null;

        if (IsTerminal(entity.Status))
            throw new InvalidOperationException($"Registration is already in a terminal status ({entity.Status}) and cannot be cancelled.");

        entity.Status = StudentRegistrationStatus.Cancelled;
        entity.UpdatedAt = DateTime.UtcNow;
        await db.SaveChangesAsync();

        return await GetByIdAsync(entity.Id);
    }

    private static StudentRegistrationStatus? GetNextStatus(StudentRegistrationStatus current) => current switch
    {
        StudentRegistrationStatus.Draft => StudentRegistrationStatus.Submitted,
        StudentRegistrationStatus.Submitted => StudentRegistrationStatus.UnderReview,
        StudentRegistrationStatus.UnderReview => StudentRegistrationStatus.Accepted,
        _ => null
    };

    private static bool IsTerminal(StudentRegistrationStatus status) => status is
        StudentRegistrationStatus.Accepted or
        StudentRegistrationStatus.Rejected or
        StudentRegistrationStatus.Cancelled;

    private async Task<string> GenerateApplicationNumberAsync()
    {
        var year = DateTime.UtcNow.Year;
        string candidate;
        do
        {
            candidate = $"REG-{year}-{Random.Shared.Next(100000, 999999)}";
        } while (await db.StudentRegistrations.AnyAsync(r => r.ApplicationNumber == candidate));

        return candidate;
    }

    private async Task<string> GenerateRegistrationNumberAsync(int facultyId)
    {
        var studyYear = await db.StudyYears.FirstOrDefaultAsync(y => y.IsCurrent)
            ?? throw new InvalidOperationException("No current study year is configured.");

        var faculty = await db.Faculties.FindAsync(facultyId)
            ?? throw new InvalidOperationException($"Faculty with id {facultyId} was not found.");

        if (string.IsNullOrWhiteSpace(faculty.PrefixNumber))
            throw new InvalidOperationException($"Faculty '{faculty.Name}' does not have a PrefixNumber configured.");

        var prefix = $"{studyYear.Name}{faculty.PrefixNumber}";

        var lastSequence = await db.StudentRegistrations
            .Where(r => r.RegistrationNumber != null && r.RegistrationNumber.StartsWith(prefix))
            .Select(r => r.RegistrationNumber!.Substring(prefix.Length))
            .ToListAsync();

        var maxSequence = lastSequence
            .Select(s => int.TryParse(s, out var n) ? n : 0)
            .DefaultIfEmpty(0)
            .Max();

        return $"{prefix}{(maxSequence + 1):D3}";
    }

    private static StudentRegistrationDto ToDtoProjection(StudentRegistration r) => new()
    {
        Id = r.Id,
        ApplicationNumber = r.ApplicationNumber,
        FullName = r.FullName,
        MotherName = r.MotherName,
        BirthPlace = r.BirthPlace,
        BirthDate = r.BirthDate,
        NationalNumber = r.NationalNumber,
        IdentityNumber = r.IdentityNumber,
        RegistrationPlace = r.RegistrationPlace,
        RegistrationNumber = r.RegistrationNumber,
        Address = r.Address,
        Phone = r.Phone,
        Mobile = r.Mobile,
        FacultyId = r.FacultyId,
        Faculty = r.Faculty == null ? null : new FacultyLookupDto
        {
            Id = r.Faculty.Id,
            Slug = r.Faculty.Slug ?? string.Empty,
            Name = r.Faculty.Name,
            Name_AR = r.Faculty.Name_AR,
            PrefixNumber = r.Faculty.PrefixNumber
        },
        AdmissionTypeId = r.AdmissionTypeId,
        AdmissionType = r.AdmissionType == null ? null : new AdmissionTypeLookupDto
        {
            Id = r.AdmissionType.Id,
            Name = r.AdmissionType.Name,
            Name_AR = r.AdmissionType.Name_AR
        },
        OfficeId = r.OfficeId,
        Office = r.Office == null ? null : new OfficeLookupDto
        {
            Id = r.Office.Id,
            Name = r.Office.Name,
            Name_AR = r.Office.Name_AR
        },
        AmountPaid = r.AmountPaid,
        Status = r.Status,
        Note = r.Note,
        HighSchoolCertificate = r.HighSchoolCertificate == null ? null : new StudentHighSchoolCertificateDto
        {
            Id = r.HighSchoolCertificate.Id,
            StudentRegistrationId = r.HighSchoolCertificate.StudentRegistrationId,
            CertificateTypeId = r.HighSchoolCertificate.CertificateTypeId,
            CertificateType = r.HighSchoolCertificate.CertificateType == null ? null : new HighSchoolCertificateTypeLookupDto
            {
                Id = r.HighSchoolCertificate.CertificateType.Id,
                Name = r.HighSchoolCertificate.CertificateType.Name,
                Name_AR = r.HighSchoolCertificate.CertificateType.Name_AR
            },
            CertificateSource = r.HighSchoolCertificate.CertificateSource,
            CertificatePlace = r.HighSchoolCertificate.CertificatePlace,
            CertificateDate = r.HighSchoolCertificate.CertificateDate,
            CertificateOrSubscriptionNumber = r.HighSchoolCertificate.CertificateOrSubscriptionNumber,
            ExamSessionId = r.HighSchoolCertificate.ExamSessionId,
            ExamSession = r.HighSchoolCertificate.ExamSession == null ? null : new ExamSessionLookupDto
            {
                Id = r.HighSchoolCertificate.ExamSession.Id,
                Name = r.HighSchoolCertificate.ExamSession.Name,
                Name_AR = r.HighSchoolCertificate.ExamSession.Name_AR
            },
            GeneralTotal = r.HighSchoolCertificate.GeneralTotal,
            Average = r.HighSchoolCertificate.Average,
            AdmissionAverageAfterLanguageExclusion = r.HighSchoolCertificate.AdmissionAverageAfterLanguageExclusion
        },
        DisplayOrder = r.DisplayOrder,
        IsActive = r.IsActive,
        CreatedAt = r.CreatedAt,
        UpdatedAt = r.UpdatedAt
    };

    private static StudentRegistrationDto ToDto(StudentRegistration r) => ToDtoProjection(r);
}
