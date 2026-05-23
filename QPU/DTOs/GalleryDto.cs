using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class GalleryDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string? Title_AR { get; set; }

    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }

    public bool IsPublished { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public List<GalleryAttachmentDto> Attachments { get; set; } = [];
}

public class GalleryAttachmentDto
{
    public int Id { get; set; }
    public int GalleryId { get; set; }
    public Guid FileManagerId { get; set; }
    public int DisplayOrder { get; set; }
    public FileManagerNodeDto? File { get; set; }
}

public class CreateGalleryRequest
{
    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Title_AR { get; set; }

    public DateOnly? DateFrom { get; set; }
    public DateOnly? DateTo { get; set; }

    public bool IsPublished { get; set; } = true;
    public int DisplayOrder { get; set; }

    public List<Guid> AttachmentIds { get; set; } = [];
}
