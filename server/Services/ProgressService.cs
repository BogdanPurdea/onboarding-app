using OnboardingApp.Api.Repositories;

namespace OnboardingApp.Api.Services;

public class ProgressService(IProgressRepository progressRepository) : IProgressService
{
    public Task<IEnumerable<int>> GetCompletedTaskIdsAsync(Guid token)
        => progressRepository.GetCompletedTaskIdsAsync(token);

    public Task SyncProgressAsync(Guid token, List<int> completedTaskIds)
        => progressRepository.SyncProgressAsync(token, completedTaskIds);
}
