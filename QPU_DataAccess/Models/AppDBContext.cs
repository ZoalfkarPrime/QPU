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

        // Faculty
        modelBuilder.Entity<Faculty>()
            .HasIndex(f => f.Slug)
            .IsUnique();

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
    }
}