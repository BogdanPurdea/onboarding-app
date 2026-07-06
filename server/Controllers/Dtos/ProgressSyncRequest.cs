namespace OnboardingApp.Api.Controllers.Dtos;

public record ProgressSyncRequest(
    string SessionToken,
    List<int> CompletedTaskIds
);
