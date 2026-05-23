using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class GalleryService(AppDBContext db, IConfiguration config) : IGalleryService
{
    private string ApiBaseUrl => config["FileManager:APIBaseURL"] ?? string.Empty;

    public IQueryable<GalleryDto> GetQueryable()
    {
        var baseUrl = ApiBaseUrl;
        return db.Galleries.OrderBy(g => g.DisplayOrder).Select(g => new GalleryDto
        {
            Id = g.Id,
            Title = g.Title,
            Title_AR = g.Title_AR,
            DateFrom = g.DateFrom,
            DateTo = g.DateTo,
            IsPublished = g.IsPublished,
            DisplayOrder = g.DisplayOrder,
            IsActive = g.IsActive,
            CreatedAt = g.CreatedAt,
            UpdatedAt = g.UpdatedAt,
            Attachments = g.Attachments
                .OrderBy(a => a.DisplayOrder)
                .Select(a => new GalleryAttachmentDto
                {
                    Id = a.Id,
                    GalleryId = a.GalleryId,
                    FileManagerId = a.FileManagerId,
                    DisplayOrder = a.DisplayOrder,
                    File = a.File == null ? null : new FileManagerNodeDto
                    {
                        Id = a.File.Id,
                        Name = a.File.Name,
                        Name_AR = a.File.Name_AR,
                        URL = a.File.URL != null ? baseUrl + a.File.URL : null,
                        Thumbnail = a.File.Thumbnail != null ? baseUrl + a.File.Thumbnail : null,
                        IsFile = a.File.IsFile,
                        FileType = a.File.FileType
                    }
                })
                .ToList()
        });
    }

    public async Task<GalleryDto?> GetByIdAsync(int id)
    {
        return await GetQueryable().FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<GalleryDto> CreateAsync(CreateGalleryRequest request)
    {
        var entity = new Gallery
        {
            Title = request.Title,
            Title_AR = request.Title_AR,
            DateFrom = request.DateFrom,
            DateTo = request.DateTo,
            IsPublished = request.IsPublished,
            DisplayOrder = request.DisplayOrder,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        db.Galleries.Add(entity);
        await db.SaveChangesAsync();

        for (int i = 0; i < request.AttachmentIds.Count; i++)
        {
            db.GalleryAttachments.Add(new GalleryAttachment
            {
                GalleryId = entity.Id,
                FileManagerId = request.AttachmentIds[i],
                DisplayOrder = i,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        await db.SaveChangesAsync();
        return await GetQueryable().FirstAsync(g => g.Id == entity.Id);
    }

    public async Task<GalleryDto?> UpdateAsync(GalleryDto dto)
    {
        var entity = await db.Galleries
            .Include(g => g.Attachments)
            .FirstOrDefaultAsync(g => g.Id == dto.Id);

        if (entity is null) return null;

        entity.Title = dto.Title;
        entity.Title_AR = dto.Title_AR;
        entity.DateFrom = dto.DateFrom;
        entity.DateTo = dto.DateTo;
        entity.IsPublished = dto.IsPublished;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;
        entity.UpdatedAt = DateTime.UtcNow;

        // Sync attachments
        var incomingIds = dto.Attachments.Select(a => a.FileManagerId).ToHashSet();
        var existingIds = entity.Attachments.Select(a => a.FileManagerId).ToHashSet();

        // Remove detached
        var toRemove = entity.Attachments.Where(a => !incomingIds.Contains(a.FileManagerId)).ToList();
        db.GalleryAttachments.RemoveRange(toRemove);

        // Add new
        var toAdd = dto.Attachments.Where(a => !existingIds.Contains(a.FileManagerId)).ToList();
        foreach (var a in toAdd)
        {
            db.GalleryAttachments.Add(new GalleryAttachment
            {
                GalleryId = entity.Id,
                FileManagerId = a.FileManagerId,
                DisplayOrder = a.DisplayOrder,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        // Update display order for existing
        foreach (var a in entity.Attachments.Where(a => incomingIds.Contains(a.FileManagerId)))
        {
            var match = dto.Attachments.FirstOrDefault(x => x.FileManagerId == a.FileManagerId);
            if (match is not null) a.DisplayOrder = match.DisplayOrder;
        }

        await db.SaveChangesAsync();
        return await GetQueryable().FirstAsync(g => g.Id == entity.Id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await db.Galleries.FindAsync(id);
        if (entity is null) return false;

        db.Galleries.Remove(entity);
        await db.SaveChangesAsync();
        return true;
    }
}
