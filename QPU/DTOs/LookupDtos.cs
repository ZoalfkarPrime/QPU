namespace QPU.DTOs;

public class FacultyLookupDto
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Name_AR { get; set; }
}

public class StudyYearLookupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Name_AR { get; set; }
    public bool IsCurrent { get; set; }
}

public class TeacherLookupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Name_AR { get; set; }
    public FileManagerNodeDto? Picture { get; set; }
}

public class CourseLookupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Name_AR { get; set; }
    public int FacultyId { get; set; }
    public int StudyYearId { get; set; }
}
