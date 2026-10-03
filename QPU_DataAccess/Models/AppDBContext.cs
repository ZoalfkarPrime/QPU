using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QPU_DataAccess.Models;

public class AppDBContext : IdentityDbContext<AppUser, AppRole, string, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppToken>
{
    public AppDBContext(DbContextOptions<AppDBContext> options)
        : base(options)
    {
    }

    public DbSet<FileManager> FileManagers => Set<FileManager>();
    public DbSet<Faculty> Faculties => Set<Faculty>();
    public DbSet<Lab> Labs => Set<Lab>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<FacultyTeacher> FacultyTeachers => Set<FacultyTeacher>();
    public DbSet<StudyYear> StudyYears => Set<StudyYear>();
    public DbSet<StudyProgram> StudyPrograms => Set<StudyProgram>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<CourseTeacher> CourseTeachers => Set<CourseTeacher>();
    public DbSet<Lecture> Lectures => Set<Lecture>();
    public DbSet<GraduatedStudent> GraduatedStudents => Set<GraduatedStudent>();
    public DbSet<ScientificResearch> ScientificResearches => Set<ScientificResearch>();
    public DbSet<Content> Contents => Set<Content>();
    public DbSet<ContentMeta> ContentMetas => Set<ContentMeta>();
    public DbSet<BestEmployee> BestEmployees => Set<BestEmployee>();
    public DbSet<Vacancy> Vacancies => Set<Vacancy>();
    public DbSet<SiteRequest> SiteRequests => Set<SiteRequest>();
    public DbSet<Gallery> Galleries => Set<Gallery>();
    public DbSet<GalleryAttachment> GalleryAttachments => Set<GalleryAttachment>();
    public DbSet<StudentRegistration> StudentRegistrations => Set<StudentRegistration>();
    public DbSet<StudentHighSchoolCertificate> StudentHighSchoolCertificates => Set<StudentHighSchoolCertificate>();
    public DbSet<AdmissionType> AdmissionTypes => Set<AdmissionType>();
    public DbSet<HighSchoolCertificateType> HighSchoolCertificateTypes => Set<HighSchoolCertificateType>();
    public DbSet<ExamSession> ExamSessions => Set<ExamSession>();
    public DbSet<Office> Offices => Set<Office>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<AppToken>()
            .HasKey(t => new { t.UserId, t.LoginProvider, t.Name, t.Device, t.Value });

        // FileManager
        modelBuilder.Entity<FileManager>()
            .HasOne(f => f.Parent)
            .WithMany(f => f.Children)
            .HasForeignKey(f => f.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        // AppUser → Faculty (scoped access)
        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.Faculty)
            .WithMany()
            .HasForeignKey(u => u.FacultyId)
            .OnDelete(DeleteBehavior.Restrict);

        // Only one superadmin allowed at the DB level
        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.IsSuperAdmin)
            .IsUnique()
            .HasFilter("[IsSuperAdmin] = 1");

        // Faculty
        modelBuilder.Entity<Faculty>()
            .HasIndex(f => f.Slug)
            .IsUnique();

        // Unique slugs for faculty-related content entities
        modelBuilder.Entity<Lab>()
            .HasIndex(l => l.Slug)
            .IsUnique()
            .HasFilter("[Slug] IS NOT NULL");

        modelBuilder.Entity<Teacher>()
            .HasIndex(t => t.Slug)
            .IsUnique()
            .HasFilter("[Slug] IS NOT NULL");

        modelBuilder.Entity<Course>()
            .HasIndex(c => c.Slug)
            .IsUnique()
            .HasFilter("[Slug] IS NOT NULL");

        modelBuilder.Entity<Lecture>()
            .HasIndex(l => l.Slug)
            .IsUnique()
            .HasFilter("[Slug] IS NOT NULL");

        modelBuilder.Entity<ScientificResearch>()
            .HasIndex(r => r.Slug)
            .IsUnique()
            .HasFilter("[Slug] IS NOT NULL");

        modelBuilder.Entity<StudyProgram>()
            .HasIndex(sp => sp.Slug)
            .IsUnique()
            .HasFilter("[Slug] IS NOT NULL");

        modelBuilder.Entity<GraduatedStudent>()
            .HasIndex(g => g.Slug)
            .IsUnique()
            .HasFilter("[Slug] IS NOT NULL");

        modelBuilder.Entity<Gallery>()
            .HasIndex(g => g.Slug)
            .IsUnique()
            .HasFilter("[Slug] IS NOT NULL");

        modelBuilder.Entity<Faculty>()
            .HasOne(f => f.Picture)
            .WithMany()
            .HasForeignKey(f => f.PictureId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Faculty>()
            .HasOne(f => f.Logo)
            .WithMany()
            .HasForeignKey(f => f.LogoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Lab
        modelBuilder.Entity<Lab>()
            .HasOne(l => l.Faculty)
            .WithMany(f => f.Labs)
            .HasForeignKey(l => l.FacultyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lab>()
            .HasOne(l => l.Picture)
            .WithMany()
            .HasForeignKey(l => l.PictureId)
            .OnDelete(DeleteBehavior.Restrict);

        // Teacher
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Picture)
            .WithMany()
            .HasForeignKey(t => t.PictureId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.CvEnglish)
            .WithMany()
            .HasForeignKey(t => t.CvEnglishId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.CvArabic)
            .WithMany()
            .HasForeignKey(t => t.CvArabicId)
            .OnDelete(DeleteBehavior.Restrict);

        // FacultyTeacher
        modelBuilder.Entity<FacultyTeacher>()
            .HasIndex(ft => new { ft.FacultyId, ft.TeacherId })
            .IsUnique();

        modelBuilder.Entity<FacultyTeacher>()
            .HasOne(ft => ft.Faculty)
            .WithMany(f => f.FacultyTeachers)
            .HasForeignKey(ft => ft.FacultyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<FacultyTeacher>()
            .HasOne(ft => ft.Teacher)
            .WithMany(t => t.FacultyTeachers)
            .HasForeignKey(ft => ft.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // Course
        modelBuilder.Entity<Course>()
            .HasOne(c => c.Faculty)
            .WithMany(f => f.Courses)
            .HasForeignKey(c => c.FacultyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.StudyYear)
            .WithMany(sy => sy.Courses)
            .HasForeignKey(c => c.StudyYearId)
            .OnDelete(DeleteBehavior.Restrict);

        // CourseTeacher
        modelBuilder.Entity<CourseTeacher>()
            .HasIndex(ct => new { ct.CourseId, ct.TeacherId })
            .IsUnique();

        modelBuilder.Entity<CourseTeacher>()
            .HasOne(ct => ct.Course)
            .WithMany(c => c.CourseTeachers)
            .HasForeignKey(ct => ct.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CourseTeacher>()
            .HasOne(ct => ct.Teacher)
            .WithMany(t => t.CourseTeachers)
            .HasForeignKey(ct => ct.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // Lecture
        modelBuilder.Entity<Lecture>()
            .HasOne(l => l.Course)
            .WithMany(c => c.Lectures)
            .HasForeignKey(l => l.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Lecture>()
            .HasOne(l => l.Teacher)
            .WithMany(t => t.Lectures)
            .HasForeignKey(l => l.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Lecture>()
            .HasOne(l => l.File)
            .WithMany()
            .HasForeignKey(l => l.FileId)
            .OnDelete(DeleteBehavior.Restrict);

        // ScientificResearch
        modelBuilder.Entity<ScientificResearch>()
            .HasOne(r => r.Faculty)
            .WithMany(f => f.ScientificResearches)
            .HasForeignKey(r => r.FacultyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ScientificResearch>()
            .HasOne(r => r.Teacher)
            .WithMany(t => t.ScientificResearches)
            .HasForeignKey(r => r.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ScientificResearch>()
            .HasOne(r => r.StudyYear)
            .WithMany(sy => sy.ScientificResearches)
            .HasForeignKey(r => r.StudyYearId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ScientificResearch>()
            .HasOne(r => r.DownloadFile)
            .WithMany()
            .HasForeignKey(r => r.DownloadFileId)
            .OnDelete(DeleteBehavior.Restrict);

        // GraduatedStudent
        modelBuilder.Entity<GraduatedStudent>()
            .HasOne(g => g.StudyYear)
            .WithMany(sy => sy.GraduatedStudents)
            .HasForeignKey(g => g.StudyYearId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<GraduatedStudent>()
            .HasOne(g => g.Faculty)
            .WithMany(f => f.GraduatedStudents)
            .HasForeignKey(g => g.FacultyId)
            .OnDelete(DeleteBehavior.Restrict);

        // StudyProgram
        modelBuilder.Entity<StudyProgram>()
            .HasOne(sp => sp.StudyYear)
            .WithMany(sy => sy.StudyPrograms)
            .HasForeignKey(sp => sp.StudyYearId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudyProgram>()
            .HasOne(sp => sp.File)
            .WithMany()
            .HasForeignKey(sp => sp.FileId)
            .OnDelete(DeleteBehavior.Restrict);

        // Content
        modelBuilder.Entity<ContentMeta>()
            .HasIndex(cm => new { cm.ContentId, cm.KeyName })
            .IsUnique();

        modelBuilder.Entity<ContentMeta>()
            .HasOne(cm => cm.Content)
            .WithMany(c => c.ContentMetas)
            .HasForeignKey(cm => cm.ContentId)
            .OnDelete(DeleteBehavior.Cascade);

        // BestEmployee
        modelBuilder.Entity<BestEmployee>()
            .HasOne(be => be.Faculty)
            .WithMany()
            .HasForeignKey(be => be.FacultyId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<BestEmployee>()
            .HasOne(be => be.StudyYear)
            .WithMany()
            .HasForeignKey(be => be.StudyYearId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BestEmployee>()
            .HasOne(be => be.Teacher)
            .WithMany()
            .HasForeignKey(be => be.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<BestEmployee>()
            .HasIndex(be => new { be.FacultyId, be.StudyYearId })
            .IsUnique();

        // Gallery
        modelBuilder.Entity<GalleryAttachment>()
            .HasOne(ga => ga.Gallery)
            .WithMany(g => g.Attachments)
            .HasForeignKey(ga => ga.GalleryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<GalleryAttachment>()
            .HasOne(ga => ga.File)
            .WithMany()
            .HasForeignKey(ga => ga.FileManagerId)
            .OnDelete(DeleteBehavior.Restrict);

        // StudentRegistration
        modelBuilder.Entity<StudentRegistration>()
            .HasIndex(sr => sr.ApplicationNumber)
            .IsUnique();

        modelBuilder.Entity<StudentRegistration>()
            .HasIndex(sr => sr.NationalNumber);

        modelBuilder.Entity<StudentRegistration>()
            .HasOne(sr => sr.Faculty)
            .WithMany()
            .HasForeignKey(sr => sr.FacultyId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentRegistration>()
            .HasOne(sr => sr.AdmissionType)
            .WithMany(at => at.StudentRegistrations)
            .HasForeignKey(sr => sr.AdmissionTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentRegistration>()
            .HasOne(sr => sr.Office)
            .WithMany(o => o.StudentRegistrations)
            .HasForeignKey(sr => sr.OfficeId)
            .OnDelete(DeleteBehavior.Restrict);

        // StudentHighSchoolCertificate (1:1 with StudentRegistration)
        modelBuilder.Entity<StudentHighSchoolCertificate>()
            .HasIndex(shc => shc.StudentRegistrationId)
            .IsUnique();

        modelBuilder.Entity<StudentHighSchoolCertificate>()
            .HasOne(shc => shc.StudentRegistration)
            .WithOne(sr => sr.HighSchoolCertificate)
            .HasForeignKey<StudentHighSchoolCertificate>(shc => shc.StudentRegistrationId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StudentHighSchoolCertificate>()
            .HasOne(shc => shc.CertificateType)
            .WithMany(ct => ct.StudentHighSchoolCertificates)
            .HasForeignKey(shc => shc.CertificateTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<StudentHighSchoolCertificate>()
            .HasOne(shc => shc.ExamSession)
            .WithMany(es => es.StudentHighSchoolCertificates)
            .HasForeignKey(shc => shc.ExamSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Seed data: AdmissionType (fixed business list)
        modelBuilder.Entity<AdmissionType>().HasData(
            new AdmissionType { Id = 1, Name = "General", Name_AR = "عامة", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new AdmissionType { Id = 2, Name = "Vacancy Filling", Name_AR = "ملء شواغر", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new AdmissionType { Id = 3, Name = "Equivalent Transfer from Syrian Universities", Name_AR = "تحويل مماثل من جامعات سورية", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new AdmissionType { Id = 4, Name = "Equivalent Transfer from Non-Syrian Universities", Name_AR = "تحويل مماثل من جامعات غير سورية", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new AdmissionType { Id = 5, Name = "Change of Registration from Syrian Universities", Name_AR = "تغيير قيد من جامعات سوريا", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new AdmissionType { Id = 6, Name = "Change of Registration from Non-Syrian Universities", Name_AR = "تغيير قيد من جامعات غير سورية", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new AdmissionType { Id = 7, Name = "Institutes and Universities Comparative Admission", Name_AR = "مفاضلة المعاهد والجامعات", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new AdmissionType { Id = 8, Name = "Arab and Foreign Students Admission", Name_AR = "مفاضلة عرب واجانب", CreatedAt = SeedDate, UpdatedAt = SeedDate }
        );

        // Seed data: HighSchoolCertificateType (fixed business list)
        modelBuilder.Entity<HighSchoolCertificateType>().HasData(
            new HighSchoolCertificateType { Id = 1, Name = "Scientific", Name_AR = "علمي", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new HighSchoolCertificateType { Id = 2, Name = "Literary", Name_AR = "أدبي", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new HighSchoolCertificateType { Id = 3, Name = "Technical", Name_AR = "تقني", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new HighSchoolCertificateType { Id = 4, Name = "Vocational", Name_AR = "فني", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new HighSchoolCertificateType { Id = 5, Name = "Other", Name_AR = "أخرى", CreatedAt = SeedDate, UpdatedAt = SeedDate }
        );

        // Seed data: ExamSession (fixed business list)
        modelBuilder.Entity<ExamSession>().HasData(
            new ExamSession { Id = 1, Name = "First Session", Name_AR = "الدورة الأولى", CreatedAt = SeedDate, UpdatedAt = SeedDate },
            new ExamSession { Id = 2, Name = "Second Session", Name_AR = "الدورة الثانية", CreatedAt = SeedDate, UpdatedAt = SeedDate }
        );
    }

    private static readonly DateTime SeedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}