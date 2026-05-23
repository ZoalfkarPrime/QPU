using QPU.DTOs;

namespace QPU.Services;

public interface IGalleryService
{
    IQueryable<GalleryDto> GetQueryable();
    Task<GalleryDto?> GetByIdAsync(int id);
    Task<GalleryDto> CreateAsync(CreateGalleryRequest request);
    Task<GalleryDto?> UpdateAsync(GalleryDto dto);
    Task<bool> DeleteAsync(int id);
}
