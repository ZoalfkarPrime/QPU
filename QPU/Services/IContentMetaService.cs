using QPU.DTOs;

namespace QPU.Services;

public interface IContentMetaService
{
    IQueryable<ContentMetaDto> GetQueryable();
    Task<ContentMetaDto?> GetByIdAsync(int id);
    Task<ContentMetaDto> CreateAsync(CreateContentMetaRequest request);
    Task<ContentMetaDto?> UpdateAsync(ContentMetaDto dto);
    Task<bool> DeleteAsync(int id);
}
