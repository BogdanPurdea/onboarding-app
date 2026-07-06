using Microsoft.EntityFrameworkCore;
using OnboardingApp.Api.Data;
using OnboardingApp.Api.Models;

namespace OnboardingApp.Api.Repositories;

public class ProgressRepository(OnboardingDbContext dbContext) : IProgressRepository
{
    public async Task<IEnumerable<int>> GetCompletedTaskIdsAsync(Guid token)
    {
        return await dbContext.OnboardingProgresses
            .Where(op => op.SessionToken == token)
            .Select(op => op.TaskId)
            .ToListAsync();
    }

    public async Task SyncProgressAsync(Guid token, List<int> completedTaskIds)
    {
        // Remove all existing records for this session token
        var existing = dbContext.OnboardingProgresses
            .Where(op => op.SessionToken == token);

        dbContext.OnboardingProgresses.RemoveRange(existing);

        // Insert the new set of completed task IDs
        var newRecords = completedTaskIds.Select(taskId => new OnboardingProgress
        {
            SessionToken = token,
            TaskId = taskId
        });

        await dbContext.OnboardingProgresses.AddRangeAsync(newRecords);
        await dbContext.SaveChangesAsync();
    }
}
