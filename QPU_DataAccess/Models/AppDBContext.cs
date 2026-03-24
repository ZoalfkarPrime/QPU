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
    public DbSet<ScientificResearch> ScientificResearches => Set<ScientificResearch>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<AppToken>()
            .HasKey(t => new { t.UserId, t.LoginProvider, t.Name, t.Device, t.Value });

        modelBuilder.Entity<FileManager>()
            .HasOne(f => f.Parent)
            .WithMany(f => f.Children)
            .HasForeignKey(f => f.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Faculty>()
            .HasIndex(f => f.Slug)
            .IsUnique();

        modelBuilder.Entity<Faculty>()
            .HasOne(f => f.Picture)
            .WithMany()
            .HasForeignKey(f => f.PictureId)
            .OnDelete(DeleteBehavior.Restrict);

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
            .HasOne(r => r.DownloadFile)
            .WithMany()
            .HasForeignKey(r => r.DownloadFileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}