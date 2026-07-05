namespace OnboardingApp.Api.Models;

public class TaskInstruction
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public OnboardingTask Task { get; set; } = null!;
    public int StepNumber { get; set; }
    public string Text { get; set; } = string.Empty;
}
