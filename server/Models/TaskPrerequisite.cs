namespace OnboardingApp.Api.Models;

public class TaskPrerequisite
{
    /// <summary>
    /// The task that must be completed first.
    /// </summary>
    public int PreDependentTaskId { get; set; }

    /// <summary>
    /// The task that is unlocked after the prerequisite is completed.
    /// </summary>
    public int PostDependentTaskId { get; set; }

    // Navigation properties
    public OnboardingTask PreDependentTask { get; set; } = null!;
    public OnboardingTask PostDependentTask { get; set; } = null!;
}
