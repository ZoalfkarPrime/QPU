namespace QPU.DTOs;

public class VisitorCounterDto
{
    public int TotalVisitors { get; set; }
    public int TodayVisitors { get; set; }
}

public class SetVisitorCountRequest
{
    public int TotalVisitors { get; set; }
}
