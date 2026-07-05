using OnboardingApp.Api.Controllers.Dtos;

namespace OnboardingApp.Api.Services;

public interface IOnboardingService
{
    Task<IEnumerable<TaskDto>> GetTasksForDepartmentAsync(int? departmentId, string? role);
    Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync();
    Task<DashboardDto> GetDepartmentDashboardAsync(string roleKey);
    Task<TaskInstructionsDto?> GetTaskInstructionsAsync(int taskId);
}
