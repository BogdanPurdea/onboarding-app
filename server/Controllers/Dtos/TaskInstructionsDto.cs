namespace OnboardingApp.Api.Controllers.Dtos;

public record TaskInstructionsDto(
    int TaskId,
    string TaskTitle,
    IReadOnlyList<string> Steps
);
