namespace OnboardingApp.Api.Models;

public class DepartmentContact
{
    public int Id { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string AvatarInitials { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string? SlackMemberId { get; set; }
    public string? GoogleMeetUrl { get; set; }
    public string PreferredCommTool { get; set; } = "slack";
    public int DisplayOrder { get; set; }
}
