namespace OnboardingApp.Api.Controllers.Dtos;

public record DepartmentDto(
    int Id,
    string Name,
    string RoleKey,
    string Tagline
);
