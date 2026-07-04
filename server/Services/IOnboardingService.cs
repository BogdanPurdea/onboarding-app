using OnboardingApp.Api.Controllers.Dtos;

namespace OnboardingApp.Api.Services;

public interface IOnboardingService
{
    Task<IEnumerable<TaskDto>> GetTasksForDepartmentAsync(int? departmentId, string? role);
}
