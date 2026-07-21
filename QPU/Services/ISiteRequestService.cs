using QPU.DTOs;

namespace QPU.Services;

public interface ISiteRequestService
{
    IQueryable<SiteRequestDto> GetQueryable();
    Task<SiteRequestDto?> GetByIdAsync(int id);
    Task<(bool Success, string? Error, SiteRequestDto? Data)> CreateEmploymentAsync(CreateEmploymentRequest request);
    Task<SiteRequestDto> CreateContactUsAsync(CreateContactUsRequest request);
    Task<SiteRequestDto?> UpdateAsync(SiteRequestDto dto);
    Task<bool> DeleteAsync(int id);
    Task<int> DeactivateContractRequestsAsync();
}
