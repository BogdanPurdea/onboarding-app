using Microsoft.EntityFrameworkCore;
using OnboardingApp.Api.Data;
using OnboardingApp.Api.Models;

namespace OnboardingApp.Api.Repositories;

public class OnboardingRepository(OnboardingDbContext dbContext) : IOnboardingRepository
{
    public async Task<IEnumerable<OnboardingTask>> GetTasksByDepartmentIdAsync(int departmentId)
    {
        return await dbContext.OnboardingTasks
            .Include(t => t.Department)
            .Include(t => t.Prerequisites)
            .Where(t => t.DepartmentId == departmentId)
            .ToListAsync();
    }

    public async Task<Department?> GetDepartmentByIdAsync(int departmentId)
    {
        return await dbContext.Departments
            .FirstOrDefaultAsync(d => d.Id == departmentId);
    }

    public async Task<Department?> GetDepartmentByNameAsync(string name)
    {
        // Case-insensitive query using EF.Functions.ILike for PostgreSQL or standard string mapping
        return await dbContext.Departments
            .FirstOrDefaultAsync(d => EF.Functions.ILike(d.Name, name));
    }

    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
        return await dbContext.Departments
            .OrderBy(d => d.Id)
            .ToListAsync();
    }

    public async Task<Department?> GetDepartmentByRoleKeyAsync(string roleKey)
    {
        return await dbContext.Departments
            .Include(d => d.Contacts.OrderBy(c => c.DisplayOrder))
            .FirstOrDefaultAsync(d => EF.Functions.ILike(d.RoleKey, roleKey));
    }
}
