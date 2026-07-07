using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using OnboardingApp.Api.Controllers;
using OnboardingApp.Api.Controllers.Dtos;
using OnboardingApp.Api.Services;
using Xunit;

namespace OnboardingApp.Tests.Controllers;

public class DepartmentsControllerTests
{
    private readonly IOnboardingService _onboardingService = Substitute.For<IOnboardingService>();
    private readonly DepartmentsController _controller;

    public DepartmentsControllerTests()
    {
        _controller = new DepartmentsController(_onboardingService);
    }

    [Fact]
    public async Task GetDepartments_ReturnsOkWithDepartmentsList()
    {
        // Arrange
        var expectedDepts = new List<DepartmentDto>
        {
            new(1, "Engineering", "engineering", "Build things")
        };
        _onboardingService.GetDepartmentsAsync().Returns(expectedDepts);

        // Act
        var result = await _controller.GetDepartments();

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(expectedDepts);
    }

    [Fact]
    public async Task GetDashboard_WithValidRoleKey_ReturnsOkWithDashboard()
    {
        // Arrange
        var roleKey = "engineering";
        var expectedDashboard = new DashboardDto(
            "Engineering",
            roleKey,
            "Build things",
            "Welcome!",
            [],
            []
        );
        _onboardingService.GetDepartmentDashboardAsync(roleKey).Returns(expectedDashboard);

        // Act
        var result = await _controller.GetDashboard(roleKey);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeEquivalentTo(expectedDashboard);
    }

    [Fact]
    public async Task GetDashboard_WithInvalidRoleKey_ReturnsNotFound()
    {
        // Arrange
        var roleKey = "unknown";
        _onboardingService.GetDepartmentDashboardAsync(roleKey)
            .Throws(new KeyNotFoundException($"Department with role key '{roleKey}' not found."));

        // Act
        var result = await _controller.GetDashboard(roleKey);

        // Assert
        var notFoundResult = result.Should().BeOfType<NotFoundObjectResult>().Subject;
        // Verify response contains the error message in the key/value structure
        notFoundResult.Value.Should().BeEquivalentTo(new { error = $"Department with role key '{roleKey}' not found." });
    }
}
