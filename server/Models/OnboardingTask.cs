namespace OnboardingApp.Api.Models;

public class OnboardingTask
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public TimelinePhase TimelinePhase { get; set; }
    public int DisplayOrder { get; set; }

    // Navigation properties
    public Department Department { get; set; } = null!;
    public ICollection<TaskPrerequisite> Prerequisites { get; set; } = [];
    public ICollection<TaskPrerequisite> Dependents { get; set; } = [];
    public ICollection<TaskInstruction> Instructions { get; set; } = [];
}
