namespace QPU.DTOs;

public class SearchResultDto
{
    public string Query { get; set; } = string.Empty;
    public int TotalCount { get; set; }
    public SearchResultGroupDto<FacultyDto> Faculties { get; set; } = new();
    public SearchResultGroupDto<LabDto> Labs { get; set; } = new();
    public SearchResultGroupDto<TeacherDto> Teachers { get; set; } = new();
    public SearchResultGroupDto<CourseDto> Courses { get; set; } = new();
    public SearchResultGroupDto<ScientificResearchDto> ScientificResearches { get; set; } = new();
    public SearchResultGroupDto<GraduatedStudentDto> GraduatedStudents { get; set; } = new();
    public SearchResultGroupDto<ContentDto> Contents { get; set; } = new();
    public SearchResultGroupDto<VacancyDto> Vacancies { get; set; } = new();
}

public class SearchResultGroupDto<T>
{
    public string Source { get; set; } = string.Empty;
    public int Count { get; set; }
    public List<T> Items { get; set; } = [];
}
