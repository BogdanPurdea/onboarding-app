using OnboardingApp.Api.Models;

namespace OnboardingApp.Api.Repositories;

public interface IOnboardingRepository
{
    Task<IEnumerable<OnboardingTask>> GetTasksByDepartmentIdAsync(int departmentId);
    Task<Department?> GetDepartmentByIdAsync(int departmentId);
    Task<Department?> GetDepartmentByNameAsync(string name);
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
    Task<Department?> GetDepartmentByRoleKeyAsync(string roleKey);
}
