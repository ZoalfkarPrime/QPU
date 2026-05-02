using QPU.DTOs;

namespace QPU.Services;

public interface IBestEmployeeService
{
    IQueryable<BestEmployeeDto> GetQueryable();
    Task<BestEmployeeDto?> GetByIdAsync(int id);
    Task<BestEmployeeDto> CreateAsync(CreateBestEmployeeRequest request);
    Task<BestEmployeeDto?> UpdateAsync(BestEmployeeDto dto);
    Task<bool> DeleteAsync(int id);
}
