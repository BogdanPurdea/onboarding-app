namespace OnboardingApp.Api.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation property
    public ICollection<OnboardingTask> OnboardingTasks { get; set; } = [];
}
