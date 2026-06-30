using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPU_DataAccess.Models;

public class FileManager : BaseEntity
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [StringLength(255)]
    public string? Name_AR { get; set; }

    public string? URL { get; set; }

    public bool IsFile { get; set; }

    public string? Thumbnail { get; set; }

    public int FileType { get; set; }

    public Guid? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))]
    public virtual FileManager? Parent { get; set; }

    public virtual ICollection<FileManager> Children { get; set; } = new List<FileManager>();
}

public class Faculty : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Slug { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Name_AR { get; set; }

    public Guid? PictureId { get; set; }

    public Guid? LogoId { get; set; }

    public bool Slider { get; set; }

    public bool IsPublished { get; set; } = true;

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    [ForeignKey(nameof(PictureId))]
    public virtual FileManager? Picture { get; set; }

    [ForeignKey(nameof(LogoId))]
    public virtual FileManager? Logo { get; set; }

    public virtual ICollection<Lab> Labs { get; set; } = new List<Lab>();
    public virtual ICollection<ScientificResearch> ScientificResearches { get; set; } = new List<ScientificResearch>();
    public virtual ICollection<FacultyTeacher> FacultyTeachers { get; set; } = new List<FacultyTeacher>();
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<GraduatedStudent> GraduatedStudents { get; set; } = new List<GraduatedStudent>();
}

public class Lab : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FacultyId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Name_AR { get; set; }

    public Guid? PictureId { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Content { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Content_AR { get; set; }

    public bool IsPublished { get; set; } = true;

    [ForeignKey(nameof(FacultyId))]
    public virtual Faculty? Faculty { get; set; }

    [ForeignKey(nameof(PictureId))]
    public virtual FileManager? Picture { get; set; }
}

public class Teacher : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? Name_AR { get; set; }

    public Guid? PictureId { get; set; }

    [MaxLength(200)]
    public string? Position { get; set; }

    [MaxLength(200)]
    public string? Position_AR { get; set; }

    [MaxLength(300)]
    public string? Specialist { get; set; }

    [MaxLength(300)]
    public string? Specialist_AR { get; set; }

    [MaxLength(300)]
    public string? ScientificDegree { get; set; }

    [MaxLength(300)]
    public string? ScientificDegree_AR { get; set; }

    [MaxLength(300)]
    public string? AcademicDegree { get; set; }

    [MaxLength(300)]
    public string? AcademicDegree_AR { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Certificates { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Certificates_AR { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Experiences { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Experiences_AR { get; set; }

    public Guid? CvEnglishId { get; set; }

    public Guid? CvArabicId { get; set; }

    public bool IsPublished { get; set; } = true;

    public bool? HasHonor { get; set; }

    [ForeignKey(nameof(PictureId))]
    public virtual FileManager? Picture { get; set; }

    [ForeignKey(nameof(CvEnglishId))]
    public virtual FileManager? CvEnglish { get; set; }

    [ForeignKey(nameof(CvArabicId))]
    public virtual FileManager? CvArabic { get; set; }

    public virtual ICollection<ScientificResearch> ScientificResearches { get; set; } = new List<ScientificResearch>();
    public virtual ICollection<FacultyTeacher> FacultyTeachers { get; set; } = new List<FacultyTeacher>();
    public virtual ICollection<CourseTeacher> CourseTeachers { get; set; } = new List<CourseTeacher>();
    public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
}

public class FacultyTeacher : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FacultyId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    [ForeignKey(nameof(FacultyId))]
    public virtual Faculty? Faculty { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public virtual Teacher? Teacher { get; set; }
}

public class StudyYear : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(20)]
    public string? Name_AR { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public bool IsCurrent { get; set; }

    public virtual ICollection<ScientificResearch> ScientificResearches { get; set; } = new List<ScientificResearch>();
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    public virtual ICollection<GraduatedStudent> GraduatedStudents { get; set; } = new List<GraduatedStudent>();
    public virtual ICollection<StudyProgram> StudyPrograms { get; set; } = new List<StudyProgram>();
}

public class GraduatedStudent : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int StudyYearId { get; set; }

    [Required]
    public int FacultyId { get; set; }

    [Required]
    [MaxLength(200)]
    public string FullName { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? FullName_AR { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    public decimal Average { get; set; }

    [MaxLength(20)]
    public string? StudentNumber { get; set; }

    public bool IsPublished { get; set; } = true;

    [ForeignKey(nameof(StudyYearId))]
    public virtual StudyYear? StudyYear { get; set; }

    [ForeignKey(nameof(FacultyId))]
    public virtual Faculty? Faculty { get; set; }
}

public class Course : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FacultyId { get; set; }

    [Required]
    public int StudyYearId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Name_AR { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Description_AR { get; set; }

    public bool IsPublished { get; set; } = true;

    [ForeignKey(nameof(FacultyId))]
    public virtual Faculty? Faculty { get; set; }

    [ForeignKey(nameof(StudyYearId))]
    public virtual StudyYear? StudyYear { get; set; }

    public virtual ICollection<CourseTeacher> CourseTeachers { get; set; } = new List<CourseTeacher>();
    public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();
}

public class CourseTeacher : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    [ForeignKey(nameof(CourseId))]
    public virtual Course? Course { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public virtual Teacher? Teacher { get; set; }
}

public class Lecture : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CourseId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Title_AR { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Content { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Content_AR { get; set; }

    public Guid? FileId { get; set; }

    public int LectureNumber { get; set; }

    public bool IsPublished { get; set; } = true;

    [ForeignKey(nameof(CourseId))]
    public virtual Course? Course { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public virtual Teacher? Teacher { get; set; }

    [ForeignKey(nameof(FileId))]
    public virtual FileManager? File { get; set; }
}

public class ScientificResearch : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FacultyId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    public int? StudyYearId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Title_AR { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Details { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Details_AR { get; set; }

    public Guid? DownloadFileId { get; set; }

    public DateTime? PublishedAt { get; set; }

    public bool IsPublished { get; set; } = true;

    [ForeignKey(nameof(FacultyId))]
    public virtual Faculty? Faculty { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public virtual Teacher? Teacher { get; set; }

    [ForeignKey(nameof(StudyYearId))]
    public virtual StudyYear? StudyYear { get; set; }

    [ForeignKey(nameof(DownloadFileId))]
    public virtual FileManager? DownloadFile { get; set; }
}

public class StudyProgram : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int StudyYearId { get; set; }

    [Required]
    [MaxLength(300)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Name_AR { get; set; }

    public Guid? FileId { get; set; }

    public bool IsPublished { get; set; } = true;

    [ForeignKey(nameof(StudyYearId))]
    public virtual StudyYear? StudyYear { get; set; }

    [ForeignKey(nameof(FileId))]
    public virtual FileManager? File { get; set; }
}

public class Content : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string ReferenceId { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string ReferenceType { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Section { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    public virtual ICollection<ContentMeta> ContentMetas { get; set; } = new List<ContentMeta>();
}

public class ContentMeta : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int ContentId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Type { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string KeyName { get; set; } = string.Empty;

    [Column(TypeName = "nvarchar(max)")]
    public string? Value { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Value_AR { get; set; }

    [ForeignKey(nameof(ContentId))]
    public virtual Content? Content { get; set; }
}

public class BestEmployee : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int FacultyId { get; set; }

    [Required]
    public int StudyYearId { get; set; }

    [Required]
    public int TeacherId { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Description_AR { get; set; }

    [ForeignKey(nameof(FacultyId))]
    public virtual Faculty? Faculty { get; set; }

    [ForeignKey(nameof(StudyYearId))]
    public virtual StudyYear? StudyYear { get; set; }

    [ForeignKey(nameof(TeacherId))]
    public virtual Teacher? Teacher { get; set; }
}

public class Gallery : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Title_AR { get; set; }

    public DateOnly? DateFrom { get; set; }

    public DateOnly? DateTo { get; set; }

    public bool IsPublished { get; set; } = true;

    public virtual ICollection<GalleryAttachment> Attachments { get; set; } = new List<GalleryAttachment>();
}

public class GalleryAttachment : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int GalleryId { get; set; }

    [Required]
    public Guid FileManagerId { get; set; }

    [ForeignKey(nameof(GalleryId))]
    public virtual Gallery? Gallery { get; set; }

    [ForeignKey(nameof(FileManagerId))]
    public virtual FileManager? File { get; set; }
}

public enum Gender { Male = 1, Female = 2 }

public enum MaritalStatus
{
    Single = 1,   // اعزب
    Married = 2   // متزوج
}

public enum RequestCategory
{
    Employment = 1,  // طلب توظيف
    ContactUs = 2,    // قالب مراسلة
    Contract = 3      // عقد
}

public class Vacancy : BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Title_AR { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Description { get; set; }

    [Column(TypeName = "nvarchar(max)")]
    public string? Description_AR { get; set; }

    public bool IsActive { get; set; } = true;

    public virtual ICollection<SiteRequest> Requests { get; set; } = new List<SiteRequest>();
}

public class SiteRequest : BaseEntity
{
    [Key]
    public int Id { get; set; }

    // نوع الطلب
    public RequestCategory Category { get; set; } = RequestCategory.Employment;

    // الوظيفة المطلوبة * (Employment only)
    public int? VacancyId { get; set; }

    // الاسم * / الاسم الكامل *
    [Required]
    [MaxLength(200)]
    public string FirstName { get; set; } = string.Empty;

    // الكنية * (Employment only)
    [MaxLength(200)]
    public string? LastName { get; set; }

    // تاريخ الميلاد (Employment only)
    public DateOnly? DateOfBirth { get; set; }

    // محل الولادة (Employment only)
    [MaxLength(300)]
    public string? PlaceOfBirth { get; set; }

    // الجنس (Employment only)
    public Gender? Gender { get; set; }

    // الجنسية (Employment only)
    [MaxLength(100)]
    public string? Nationality { get; set; }

    // رقم الهاتف
    [MaxLength(50)]
    public string? PhoneNumber { get; set; }

    // البريد الإلكتروني
    [MaxLength(200)]
    public string? Email { get; set; }

    // الحالة الاجتماعية (Employment only — اعزب / متزوج)
    public MaritalStatus? MaritalStatus { get; set; }

    // أرفق سيرتك الذاتية (Employment only — pdf/word max 8MB)
    public Guid? CvFileId { get; set; }

    // عنوان الرسالة * (ContactUs only)
    [MaxLength(300)]
    public string? MessageTitle { get; set; }

    // نص الرسالة (ContactUs only)
    [Column(TypeName = "nvarchar(max)")]
    public string? MessageBody { get; set; }

    public int? ContractFacultyId { get; set; }

    [ForeignKey(nameof(ContractFacultyId))]
    public virtual Faculty? ContractFaculty { get; set; }


    public string? ContractScientificDegree { get; set; }

    public string? ContractSpecialist { get; set; }

    public string? ContractJob { get; set; }

    public bool? HasContractScientificDegreeApproved { get; set; }

    public bool? HasContractExperience { get; set; }
    public string? ContractExperiences { get; set; }
    public string? ContractLanguages { get; set; }

    public string? ContractCurrentPlace { get; set; }

    public bool? ContractFulltimeJob { get; set; }// عقد جزئي او كامل

    public bool? HasContractAnotherJob { get; set; }

    public bool IsDeleted { get; set; } = false;

    [ForeignKey(nameof(VacancyId))]
    public virtual Vacancy? Vacancy { get; set; }

    [ForeignKey(nameof(CvFileId))]
    public virtual FileManager? CvFile { get; set; }

    public Guid? DegreeFileId { get; set; }

    [ForeignKey(nameof(DegreeFileId))]
    public virtual FileManager? DegreeFile { get; set; }
}