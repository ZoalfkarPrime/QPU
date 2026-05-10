using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class SiteRequestService(AppDBContext db, IFileManagerService fileManagerService, IConfiguration config) : ISiteRequestService
{
    private string ApiBaseUrl => config["FileManager:APIBaseURL"] ?? string.Empty;

    public IQueryable<SiteRequestDto> GetQueryable()
    {
        var baseUrl = ApiBaseUrl;
        return db.SiteRequests.Select(r => new SiteRequestDto
        {
            Id = r.Id,
            Category = r.Category,
            VacancyId = r.VacancyId,
            Vacancy = r.Vacancy == null ? null : new VacancyDto
            {
                Id = r.Vacancy.Id,
                Title = r.Vacancy.Title,
                Title_AR = r.Vacancy.Title_AR,
                IsActive = r.Vacancy.IsActive
            },
            FirstName = r.FirstName,
            LastName = r.LastName,
            PhoneNumber = r.PhoneNumber,
            Email = r.Email,
            DateOfBirth = r.DateOfBirth,
            PlaceOfBirth = r.PlaceOfBirth,
            Gender = r.Gender,
            Nationality = r.Nationality,
            MaritalStatus = r.MaritalStatus,
            CvFileId = r.CvFileId,
            CvFile = r.CvFile == null ? null : new FileManagerNodeDto
            {
                Id = r.CvFile.Id,
                Name = r.CvFile.Name,
                Name_AR = r.CvFile.Name_AR,
                URL = r.CvFile.URL != null ? baseUrl + r.CvFile.URL : null,
                Thumbnail = r.CvFile.Thumbnail != null ? baseUrl + r.CvFile.Thumbnail : null,
                IsFile = r.CvFile.IsFile,
                FileType = r.CvFile.FileType
            },
            MessageTitle = r.MessageTitle,
            MessageBody = r.MessageBody,
            IsActive = r.IsActive,
            DisplayOrder = r.DisplayOrder,
            CreatedAt = r.CreatedAt,
            UpdatedAt = r.UpdatedAt
        });
    }

    public async Task<SiteRequestDto?> GetByIdAsync(int id) =>
        await GetQueryable().FirstOrDefaultAsync(r => r.Id == id);

    public async Task<(bool Success, string? Error, SiteRequestDto? Data)> CreateEmploymentAsync(CreateEmploymentRequest request)
    {
        // Upload CV file first if provided, get its FileManager ID
        Guid? cvFileId = null;
        if (request.CvFile is not null)
        {
            var folderId = await fileManagerService.GetOrCreateFolderAsync("Hiring Requests CVs");
            var (success, error, uploaded) = await fileManagerService.UploadSingleAsync(request.CvFile, folderId);
            if (!success)
                return (false, error, null);
            cvFileId = uploaded!.Id;
        }

        var entity = new SiteRequest
        {
            Category = RequestCategory.Employment,
            VacancyId = request.VacancyId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            PlaceOfBirth = request.PlaceOfBirth,
            Gender = request.Gender,
            Nationality = request.Nationality,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            MaritalStatus = request.MaritalStatus,
            CvFileId = cvFileId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.SiteRequests.Add(entity);
        await db.SaveChangesAsync();
        var dto = await GetQueryable().FirstAsync(r => r.Id == entity.Id);
        return (true, null, dto);
    }

    public async Task<SiteRequestDto> CreateContactUsAsync(CreateContactUsRequest request)
    {
        var entity = new SiteRequest
        {
            Category = RequestCategory.ContactUs,
            FirstName = request.FirstName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            MessageTitle = request.MessageTitle,
            MessageBody = request.MessageBody,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.SiteRequests.Add(entity);
        await db.SaveChangesAsync();
        return await GetQueryable().FirstAsync(r => r.Id == entity.Id);
    }

    public async Task<SiteRequestDto?> UpdateAsync(SiteRequestDto dto)
    {
        var entity = await db.SiteRequests.FindAsync(dto.Id);
        if (entity is null) return null;

        entity.VacancyId = dto.VacancyId;
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.PhoneNumber = dto.PhoneNumber;
        entity.Email = dto.Email;
        entity.DateOfBirth = dto.DateOfBirth;
        entity.PlaceOfBirth = dto.PlaceOfBirth;
        entity.Gender = dto.Gender;
        entity.Nationality = dto.Nationality;
        entity.MaritalStatus = dto.MaritalStatus;
        entity.CvFileId = dto.CvFileId;
        entity.MessageTitle = dto.MessageTitle;
        entity.MessageBody = dto.MessageBody;
        entity.IsActive = dto.IsActive;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return await GetQueryable().FirstAsync(r => r.Id == entity.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.SiteRequests.FindAsync(id);
        if (entity is null) return false;

        db.SiteRequests.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
