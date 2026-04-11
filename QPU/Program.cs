using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
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

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();

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
builder.Services.AddScoped<IContentService, ContentService>();
builder.Services.AddScoped<IContentMetaService, ContentMetaService>();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? ["*"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        if (allowedOrigins.Length == 1 && allowedOrigins[0] == "*")
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        else
            policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        var logger = context.RequestServices.GetRequiredService<ILoggerFactory>().CreateLogger("GlobalException");
        var exceptionFeature = context.Features.Get<IExceptionHandlerPathFeature>();

        if (exceptionFeature?.Error is not null)
        {
            logger.LogError(
                exceptionFeature.Error,
                "Unhandled exception at {Path}. TraceId: {TraceId}",
                exceptionFeature.Path,
                context.TraceIdentifier);
        }

        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            message = "An unexpected error occurred.",
            traceId = context.TraceIdentifier
        });
    });
});

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

app.UseCors("CorsPolicy");

var uploadPath = builder.Configuration["FileManager:UploadPath"];
if (!string.IsNullOrWhiteSpace(uploadPath))
{
    Directory.CreateDirectory(uploadPath);

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(uploadPath),
        RequestPath = "/uploads"
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
