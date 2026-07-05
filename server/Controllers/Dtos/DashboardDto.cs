namespace OnboardingApp.Api.Controllers.Dtos;

public record ContactDto(
    string Name,
    string Role,
    string AvatarInitials,
    string Email,
    string Bio,
    string? SlackMemberId,
    string? GoogleMeetUrl,
    string PreferredCommTool
);

public record DayScheduleDto(
    string Day,
    string Type  // "office" | "remote"
);

public record DashboardDto(
    string DepartmentName,
    string RoleKey,
    string Tagline,
    string WelcomeMessage,
    IReadOnlyList<DayScheduleDto> WeeklySchedule,
    IReadOnlyList<ContactDto> Contacts
);
