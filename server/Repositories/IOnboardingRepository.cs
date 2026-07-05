using OnboardingApp.Api.Models;

namespace OnboardingApp.Api.Repositories;

public interface IOnboardingRepository
{
    Task<IEnumerable<OnboardingTask>> GetTasksByDepartmentIdAsync(int departmentId);
    Task<OnboardingTask?> GetTaskByIdAsync(int id);
    Task<Department?> GetDepartmentByIdAsync(int departmentId);
    Task<Department?> GetDepartmentByNameAsync(string name);
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    Task<Department?> GetDepartmentByRoleKeyAsync(string roleKey);
    Task<IEnumerable<TaskInstruction>> GetTaskInstructionsByTaskIdAsync(int taskId);
}
