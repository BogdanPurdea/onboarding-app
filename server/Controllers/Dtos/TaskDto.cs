namespace OnboardingApp.Api.Controllers.Dtos;

public class TaskDto
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int TimelinePhase { get; set; }
    public int DisplayOrder { get; set; }
    public List<int> PrerequisiteTaskIds { get; set; } = [];
}
