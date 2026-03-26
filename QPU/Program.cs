using Microsoft.EntityFrameworkCore;
using QPU.Services;
using QPU_DataAccess.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFacultyService, FacultyService>();
builder.Services.AddScoped<IFileManagerService, FileManagerService>();
builder.Services.AddScoped<ILabService, LabService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IFacultyTeacherService, FacultyTeacherService>();
builder.Services.AddScoped<IStudyYearService, StudyYearService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ICourseTeacherService, CourseTeacherService>();
builder.Services.AddScoped<ILectureService, LectureService>();
builder.Services.AddScoped<IScientificResearchService, ScientificResearchService>();
builder.Services.AddScoped<IGraduatedStudentService, GraduatedStudentService>();
builder.Services.AddScoped<IStudyProgramService, StudyProgramService>();

var app = builder.Build();

var applyMigrationsOnStartup = builder.Configuration.GetValue<bool>("Database:ApplyMigrationsOnStartup");

if (applyMigrationsOnStartup)
{
    await using var scope = app.Services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDBContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseMigration");

    const int maxAttempts = 10;

    for (var attempt = 1; attempt <= maxAttempts; attempt++)
    {
        try
        {
            await dbContext.Database.MigrateAsync();
            break;
        }
        catch (Exception ex) when (attempt < maxAttempts)
        {
            logger.LogWarning(ex, "Database migration attempt {Attempt} of {MaxAttempts} failed. Retrying in 5 seconds.", attempt, maxAttempts);
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
