using System.ComponentModel.DataAnnotations;

namespace QPU.DTOs;

public class FileManagerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? URL { get; set; }
    public bool IsFile { get; set; }
    public string? Thumbnail { get; set; }
    public int FileType { get; set; }
    public Guid? ParentId { get; set; }
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class UploadFileManagerRequest
{
    public Guid? Id { get; set; }
    public string? URL { get; set; }
    public List<IFormFile>? Files { get; set; }
    public Guid? ParentId { get; set; }
    public string? Name { get; set; }
    public bool IsFile { get; set; }
    public string? Thumbnail { get; set; }
}

public class UpdateFileManagerRequest
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public Guid? ParentId { get; set; }
}
