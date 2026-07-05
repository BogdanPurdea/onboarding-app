using OnboardingApp.Api.Controllers.Dtos;
using OnboardingApp.Api.Models;
using OnboardingApp.Api.Repositories;

namespace OnboardingApp.Api.Services;

public class OnboardingService(IOnboardingRepository repository) : IOnboardingService
{
    public async Task<IEnumerable<TaskDto>> GetTasksForDepartmentAsync(int? departmentId, string? role)
    {
        Department? department = null;

        if (departmentId.HasValue)
        {
            department = await repository.GetDepartmentByIdAsync(departmentId.Value);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {departmentId.Value} not found.");
            }
        }
        else if (!string.IsNullOrWhiteSpace(role))
        {
            department = await repository.GetDepartmentByNameAsync(role.Trim());
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with role '{role}' not found.");
            }
        }
        else
        {
            throw new ArgumentException("Either departmentId or role query parameter must be provided.");
        }

        var tasks = await repository.GetTasksByDepartmentIdAsync(department.Id);

        // Sort tasks by TimelinePhase (Ascending), then by DisplayOrder (Ascending)
        return tasks
            .OrderBy(t => t.TimelinePhase)
            .ThenBy(t => t.DisplayOrder)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                DepartmentId = t.DepartmentId,
                DepartmentName = t.Department.Name,
                Title = t.Title,
                Description = t.Description,
                TimelinePhase = (int)t.TimelinePhase,
                DisplayOrder = t.DisplayOrder,
                PrerequisiteTaskIds = t.Prerequisites
                    .Select(p => p.PreDependentTaskId)
                    .ToList()
            });
    }

    public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
    {
        var departments = await repository.GetAllDepartmentsAsync();
        return departments.Select(d => new DepartmentDto(d.Id, d.Name, d.RoleKey, d.Tagline));
    }

    public async Task<DashboardDto> GetDepartmentDashboardAsync(string roleKey)
    {
        var department = await repository.GetDepartmentByRoleKeyAsync(roleKey.Trim().ToLower());
        if (department == null)
        {
            throw new KeyNotFoundException($"Department with role key '{roleKey}' not found.");
        }

        var schedule = new List<DayScheduleDto>
        {
            new("Monday",    department.MondaySchedule),
            new("Tuesday",   department.TuesdaySchedule),
            new("Wednesday", department.WednesdaySchedule),
            new("Thursday",  department.ThursdaySchedule),
            new("Friday",    department.FridaySchedule),
        };

        var contacts = department.Contacts
            .Select(c => new ContactDto(c.Name, c.Role, c.AvatarInitials, c.Email, c.Bio, c.SlackMemberId, c.GoogleMeetUrl, c.PreferredCommTool))
            .ToList();

        return new DashboardDto(
            department.Name,
            department.RoleKey,
            department.Tagline,
            department.WelcomeMessage,
            schedule,
            contacts
        );
    }
}
