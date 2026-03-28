using QPU.DTOs;

namespace QPU.Services;

public interface IContentService
{
    IQueryable<ContentDto> GetQueryable();
    Task<ContentDto?> GetByIdAsync(int id);
    Task<ContentDto> CreateAsync(CreateContentRequest request);
    Task<ContentDto?> UpdateAsync(ContentDto dto);
    Task<bool> DeleteAsync(int id);
}
