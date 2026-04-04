namespace QPU.DTOs;

public class FileManagerNodeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Name_AR { get; set; }
    public string? URL { get; set; }
    public string? Thumbnail { get; set; }
    public bool IsFile { get; set; }
    public int FileType { get; set; }
}
