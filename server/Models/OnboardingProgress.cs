namespace OnboardingApp.Api.Models;

public class OnboardingProgress
{
    public Guid SessionToken { get; set; }
    public int TaskId { get; set; }

    // Navigation property
    public OnboardingTask Task { get; set; } = null!;
}
