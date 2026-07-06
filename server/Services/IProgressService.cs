namespace OnboardingApp.Api.Services;

public interface IProgressService
{
    Task<IEnumerable<int>> GetCompletedTaskIdsAsync(Guid token);
    Task SyncProgressAsync(Guid token, List<int> completedTaskIds);
}
