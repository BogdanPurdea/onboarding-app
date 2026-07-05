namespace OnboardingApp.Api.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string RoleKey { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string WelcomeMessage { get; set; } = string.Empty;

    // Hybrid schedule — each column stores either "office" or "remote"
    public string MondaySchedule { get; set; } = "office";
    public string TuesdaySchedule { get; set; } = "office";
    public string WednesdaySchedule { get; set; } = "office";
    public string ThursdaySchedule { get; set; } = "remote";
    public string FridaySchedule { get; set; } = "remote";

    // Navigation properties
    public ICollection<OnboardingTask> OnboardingTasks { get; set; } = [];
    public ICollection<DepartmentContact> Contacts { get; set; } = [];
}
