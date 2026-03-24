using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPU_DataAccess.Models;

public class FileManager
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    public string? URL { get; set; }

    public bool IsFile { get; set; }

    public string? Thumbnail { get; set; }

    public int FileType { get; set; }

    public Guid? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public virtual FileManager? Parent { get; set; }

    public virtual ICollection<FileManager> Children { get; set; } = new List<FileManager>();
}

public class Faculty
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Slug { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public Guid? PictureId { get; set; }

    public bool Slider { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsPublished { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [ForeignKey(nameof(PictureId))]
    public virtual FileManager? Picture { get; set; }

    public virtual ICollection<Lab> Labs { get; set; } = new List<Lab>();
    public virtual ICollection<ScientificResearch> ScientificResearches { get; set; } = new List<ScientificResearch>();
}

public class Lab
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FacultyId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public Guid? PictureId { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Content { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsPublished { get; set; } = true;

    [ForeignKey(nameof(FacultyId))]
    public virtual Faculty? Faculty { get; set; }

    [ForeignKey(nameof(PictureId))]
    public virtual FileManager? Picture { get; set; }
}

public class Teacher
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public Guid? PictureId { get; set; }

    [MaxLength(200)]
    public string? Position { get; set; }

    [MaxLength(300)]
    public string? Specialist { get; set; }

    [MaxLength(300)]
    public string? ScientificDegree { get; set; }

    [MaxLength(300)]
    public string? AcademicDegree { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Certificates { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Experiences { get; set; }

    public Guid? CvEnglishId { get; set; }

    public Guid? CvArabicId { get; set; }

    public bool IsPublished { get; set; } = true;

    [ForeignKey(nameof(PictureId))]
    public virtual FileManager? Picture { get; set; }

    [ForeignKey(nameof(CvEnglishId))]
    public virtual FileManager? CvEnglish { get; set; }

    [ForeignKey(nameof(CvArabicId))]
    public virtual FileManager? CvArabic { get; set; }

    public virtual ICollection<ScientificResearch> ScientificResearches { get; set; } = new List<ScientificResearch>();
}

public class ScientificResearch
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FacultyId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(max)")]
    public string? Details { get; set; }

    public Guid? DownloadFileId { get; set; }

    public DateTime? PublishedAt { get; set; }

    public bool IsPublished { get; set; } = true;

    [ForeignKey(nameof(FacultyId))]
    public virtual Faculty? Faculty { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public virtual Teacher? Teacher { get; set; }

    [ForeignKey(nameof(DownloadFileId))]
    public virtual FileManager? DownloadFile { get; set; }
}