using QPU.DTOs;

namespace QPU.Services;

public interface IVisitorCounterService
{
    Task<VisitorCounterDto> GetStatsAsync();
    Task<VisitorCounterDto> TrackVisitAsync();
    Task<VisitorCounterDto> SetCountAsync(int totalVisitors);
}
