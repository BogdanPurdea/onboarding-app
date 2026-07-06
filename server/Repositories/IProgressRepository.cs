namespace OnboardingApp.Api.Repositories;

public interface IProgressRepository
{
    Task<IEnumerable<int>> GetCompletedTaskIdsAsync(Guid token);
    Task SyncProgressAsync(Guid token, List<int> completedTaskIds);
}
