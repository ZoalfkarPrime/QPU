using QPU.DTOs;

namespace QPU.Services;

public interface IStudentRegistrationService
{
    IQueryable<StudentRegistrationDto> GetQueryable();
    Task<StudentRegistrationDto?> GetByIdAsync(int id);
    Task<StudentRegistrationDto> CreateAsync(CreateStudentRegistrationRequest request);
    Task<StudentRegistrationDto?> UpdateAsync(StudentRegistrationDto dto);
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Advances the registration to the next sequential status (Draft -&gt; Submitted -&gt; UnderReview -&gt; Accepted).
    /// Returns null if not found, or throws InvalidOperationException if there is no next step (e.g. already terminal).
    /// </summary>
    Task<StudentRegistrationDto?> AdvanceStatusAsync(int id);

    /// <summary>
    /// Cancels the registration. Allowed from any non-terminal status (Draft/Submitted/UnderReview).
    /// Returns null if not found, or throws InvalidOperationException if already terminal.
    /// </summary>
    Task<StudentRegistrationDto?> CancelAsync(int id);
}
