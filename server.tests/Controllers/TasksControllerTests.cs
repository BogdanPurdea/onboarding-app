using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using OnboardingApp.Api.Controllers;
using OnboardingApp.Api.Controllers.Dtos;
using OnboardingApp.Api.Services;
using Xunit;

namespace OnboardingApp.Tests.Controllers;

public class TasksControllerTests
{
    private readonly IOnboardingService _onboardingService = Substitute.For<IOnboardingService>();
    private readonly TasksController _controller;

    public TasksControllerTests()
    {
        _controller = new TasksController(_onboardingService);
    }

    [Fact]
    public async Task GetTasks_Success_ReturnsOkWithTasks()
    {
        // Arrange
        var expectedTasks = new List<TaskDto>
        {
            new() { Id = 1, Title = "Task 1" }
        };
        _onboardingService.GetTasksForDepartmentAsync(1, null).Returns(expectedTasks);

        // Act
        var result = await _controller.GetTasks(1, null);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(expectedTasks);
    }

    [Fact]
    public async Task GetTasks_ArgumentException_ReturnsBadRequest()
    {
        // Arrange
        _onboardingService.GetTasksForDepartmentAsync(null, null)
            .Throws(new ArgumentException("Either departmentId or role query parameter must be provided."));

        // Act
        var result = await _controller.GetTasks(null, null);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.Value.Should().Be("Either departmentId or role query parameter must be provided.");
    }

    [Fact]
    public async Task GetTasks_KeyNotFoundException_ReturnsNotFound()
    {
        // Arrange
        _onboardingService.GetTasksForDepartmentAsync(99, null)
            .Throws(new KeyNotFoundException("Department with ID 99 not found."));

        // Act
        var result = await _controller.GetTasks(99, null);

        // Assert
        var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFoundResult.Value.Should().Be("Department with ID 99 not found.");
    }

    [Fact]
    public async Task GetTaskInstructions_Success_ReturnsOkWithInstructions()
    {
        // Arrange
        var expectedInstructions = new TaskInstructionsDto(1, "Task 1", ["Step 1", "Step 2"]);
        _onboardingService.GetTaskInstructionsAsync(1).Returns(expectedInstructions);

        // Act
        var result = await _controller.GetTaskInstructions(1);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(expectedInstructions);
    }

    [Fact]
    public async Task GetTaskInstructions_NotFound_ReturnsNotFound()
    {
        // Arrange
        _onboardingService.GetTaskInstructionsAsync(99).Returns((TaskInstructionsDto?)null);

        // Act
        var result = await _controller.GetTaskInstructions(99);

        // Assert
        var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        notFoundResult.Value.Should().Be("Task with ID 99 not found.");
    }
}
