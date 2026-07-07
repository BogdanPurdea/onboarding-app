using FluentAssertions;
using NSubstitute;
using OnboardingApp.Api.Repositories;
using OnboardingApp.Api.Services;
using Xunit;

namespace OnboardingApp.Tests.Services;

public class ProgressServiceTests
{
    private readonly IProgressRepository _repository = Substitute.For<IProgressRepository>();
    private readonly ProgressService _service;

    public ProgressServiceTests()
    {
        _service = new ProgressService(_repository);
    }

    [Fact]
    public async Task GetCompletedTaskIdsAsync_CallsRepositoryAndReturnsResult()
    {
        // Arrange
        var token = Guid.NewGuid();
        var expectedTaskIds = new List<int> { 1, 2, 3 };
        _repository.GetCompletedTaskIdsAsync(token).Returns(expectedTaskIds);

        // Act
        var result = await _service.GetCompletedTaskIdsAsync(token);

        // Assert
        result.Should().BeEquivalentTo(expectedTaskIds);
        await _repository.Received(1).GetCompletedTaskIdsAsync(token);
    }

    [Fact]
    public async Task SyncProgressAsync_CallsRepositoryWithGivenParameters()
    {
        // Arrange
        var token = Guid.NewGuid();
        var taskIds = new List<int> { 1, 4, 5 };

        // Act
        await _service.SyncProgressAsync(token, taskIds);

        // Assert
        await _repository.Received(1).SyncProgressAsync(token, taskIds);
    }
}
