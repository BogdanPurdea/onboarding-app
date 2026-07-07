using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using OnboardingApp.Api.Controllers;
using OnboardingApp.Api.Controllers.Dtos;
using OnboardingApp.Api.Services;
using Xunit;

namespace OnboardingApp.Tests.Controllers;

public class ProgressControllerTests
{
    private readonly IProgressService _progressService = Substitute.For<IProgressService>();
    private readonly ProgressController _controller;

    public ProgressControllerTests()
    {
        _controller = new ProgressController(_progressService);
    }

    [Fact]
    public async Task SyncProgress_WithValidRequest_ReturnsOk()
    {
        // Arrange
        var token = Guid.NewGuid();
        var request = new ProgressSyncRequest(token.ToString(), [1, 2, 3]);

        // Act
        var result = await _controller.SyncProgress(request);

        // Assert
        result.Should().BeOfType<OkResult>();
        await _progressService.Received(1).SyncProgressAsync(token, request.CompletedTaskIds);
    }

    [Fact]
    public async Task SyncProgress_WithInvalidToken_ReturnsBadRequest()
    {
        // Arrange
        var request = new ProgressSyncRequest("invalid-guid", [1, 2, 3]);

        // Act
        var result = await _controller.SyncProgress(request);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.Value.Should().Be("Invalid session token format. A valid GUID is required.");
        await _progressService.DidNotReceiveWithAnyArgs().SyncProgressAsync(Arg.Any<Guid>(), Arg.Any<List<int>>());
    }

    [Fact]
    public async Task GetProgress_WithValidToken_ReturnsOkWithCompletedTaskIds()
    {
        // Arrange
        var token = Guid.NewGuid();
        var expectedTaskIds = new List<int> { 1, 2, 3 };
        _progressService.GetCompletedTaskIdsAsync(token).Returns(expectedTaskIds);

        // Act
        var result = await _controller.GetProgress(token.ToString());

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(expectedTaskIds);
    }

    [Fact]
    public async Task GetProgress_WithInvalidToken_ReturnsBadRequest()
    {
        // Act
        var result = await _controller.GetProgress("invalid-guid");

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.Value.Should().Be("Invalid session token format. A valid GUID is required.");
        await _progressService.DidNotReceiveWithAnyArgs().GetCompletedTaskIdsAsync(Arg.Any<Guid>());
    }
}
