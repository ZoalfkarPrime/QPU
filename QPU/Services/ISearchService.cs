using QPU.DTOs;

namespace QPU.Services;

public interface ISearchService
{
    Task<SearchResultDto> SearchAsync(string query);
}
