using Microsoft.EntityFrameworkCore;
using QPU.DTOs;
using QPU_DataAccess.Models;

namespace QPU.Services;

public class SearchService(
    IFacultyService facultyService,
    ILabService labService,
    ITeacherService teacherService,
    ICourseService courseService,
    IScientificResearchService scientificResearchService,
    IGraduatedStudentService graduatedStudentService,
    IContentService contentService,
    IVacancyService vacancyService) : ISearchService
{
    public async Task<SearchResultDto> SearchAsync(string query)
    {
        var q = query.Trim().ToLower();

        var results = await Task.WhenAll(
            SearchFacultiesAsync(q),
            SearchLabsAsync(q),
            SearchTeachersAsync(q),
            SearchCoursesAsync(q),
            SearchScientificResearchesAsync(q),
            SearchGraduatedStudentsAsync(q),
            SearchContentsAsync(q),
            SearchVacanciesAsync(q)
        );

        var faculties          = (SearchResultGroupDto<FacultyDto>)results[0];
        var labs               = (SearchResultGroupDto<LabDto>)results[1];
        var teachers           = (SearchResultGroupDto<TeacherDto>)results[2];
        var courses            = (SearchResultGroupDto<CourseDto>)results[3];
        var researches         = (SearchResultGroupDto<ScientificResearchDto>)results[4];
        var graduatedStudents  = (SearchResultGroupDto<GraduatedStudentDto>)results[5];
        var contents           = (SearchResultGroupDto<ContentDto>)results[6];
        var vacancies          = (SearchResultGroupDto<VacancyDto>)results[7];

        return new SearchResultDto
        {
            Query = query,
            TotalCount = faculties.Count + labs.Count + teachers.Count + courses.Count
                       + researches.Count + graduatedStudents.Count + contents.Count + vacancies.Count,
            Faculties         = faculties,
            Labs              = labs,
            Teachers          = teachers,
            Courses           = courses,
            ScientificResearches = researches,
            GraduatedStudents = graduatedStudents,
            Contents          = contents,
            Vacancies         = vacancies
        };
    }

    // ── Per-entity search helpers ────────────────────────────────────────────

    private async Task<object> SearchFacultiesAsync(string q)
    {
        var items = await facultyService.GetQueryable()
            .Where(f => f.IsActive &&
                (f.Name.ToLower().Contains(q) ||
                (f.Name_AR != null && f.Name_AR.ToLower().Contains(q)) ||
                (f.Slug.ToLower().Contains(q))))
            .ToListAsync();

        return Group<FacultyDto>("Faculties", items);
    }

    private async Task<object> SearchLabsAsync(string q)
    {
        var items = await labService.GetQueryable()
            .Where(l => l.IsActive &&
                (l.Name.ToLower().Contains(q) ||
                (l.Name_AR != null && l.Name_AR.ToLower().Contains(q)) ||
                (l.Content != null && l.Content.ToLower().Contains(q)) ||
                (l.Content_AR != null && l.Content_AR.ToLower().Contains(q))))
            .ToListAsync();

        return Group<LabDto>("Labs", items);
    }

    private async Task<object> SearchTeachersAsync(string q)
    {
        var items = await teacherService.GetQueryable()
            .Where(t => t.IsPublished &&
                (t.Name.ToLower().Contains(q) ||
                (t.Name_AR != null && t.Name_AR.ToLower().Contains(q)) ||
                (t.Position != null && t.Position.ToLower().Contains(q)) ||
                (t.Position_AR != null && t.Position_AR.ToLower().Contains(q)) ||
                (t.Specialist != null && t.Specialist.ToLower().Contains(q)) ||
                (t.Specialist_AR != null && t.Specialist_AR.ToLower().Contains(q)) ||
                (t.ScientificDegree != null && t.ScientificDegree.ToLower().Contains(q)) ||
                (t.ScientificDegree_AR != null && t.ScientificDegree_AR.ToLower().Contains(q))))
            .ToListAsync();

        return Group<TeacherDto>("Teachers", items);
    }

    private async Task<object> SearchCoursesAsync(string q)
    {
        var items = await courseService.GetQueryable()
            .Where(c => c.IsActive &&
                (c.Name.ToLower().Contains(q) ||
                (c.Name_AR != null && c.Name_AR.ToLower().Contains(q)) ||
                (c.Description != null && c.Description.ToLower().Contains(q)) ||
                (c.Description_AR != null && c.Description_AR.ToLower().Contains(q))))
            .ToListAsync();

        return Group<CourseDto>("Courses", items);
    }

    private async Task<object> SearchScientificResearchesAsync(string q)
    {
        var items = await scientificResearchService.GetQueryable()
            .Where(r => r.IsActive &&
                (r.Title.ToLower().Contains(q) ||
                (r.Title_AR != null && r.Title_AR.ToLower().Contains(q)) ||
                (r.Details != null && r.Details.ToLower().Contains(q)) ||
                (r.Details_AR != null && r.Details_AR.ToLower().Contains(q))))
            .ToListAsync();

        return Group<ScientificResearchDto>("ScientificResearches", items);
    }

    private async Task<object> SearchGraduatedStudentsAsync(string q)
    {
        var items = await graduatedStudentService.GetQueryable()
            .Where(g => g.IsActive &&
                (g.FullName.ToLower().Contains(q) ||
                (g.FullName_AR != null && g.FullName_AR.ToLower().Contains(q)) ||
                (g.StudentNumber != null && g.StudentNumber.ToLower().Contains(q))))
            .ToListAsync();

        return Group<GraduatedStudentDto>("GraduatedStudents", items);
    }

    private async Task<object> SearchContentsAsync(string q)
    {
        var items = await contentService.GetQueryable()
            .Where(c => c.IsActive &&
                (c.Title.ToLower().Contains(q) ||
                c.Section.ToLower().Contains(q) ||
                c.ReferenceType.ToLower().Contains(q)))
            .ToListAsync();

        return Group<ContentDto>("Contents", items);
    }

    private async Task<object> SearchVacanciesAsync(string q)
    {
        var items = await vacancyService.GetQueryable()
            .Where(v => v.IsActive &&
                (v.Title.ToLower().Contains(q) ||
                (v.Title_AR != null && v.Title_AR.ToLower().Contains(q)) ||
                (v.Description != null && v.Description.ToLower().Contains(q)) ||
                (v.Description_AR != null && v.Description_AR.ToLower().Contains(q))))
            .ToListAsync();

        return Group<VacancyDto>("Vacancies", items);
    }

    // ── Helper ───────────────────────────────────────────────────────────────

    private static SearchResultGroupDto<T> Group<T>(string source, List<T> items) => new()
    {
        Source = source,
        Count  = items.Count,
        Items  = items
    };
}
