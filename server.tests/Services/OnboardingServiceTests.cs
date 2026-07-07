using FluentAssertions;
using NSubstitute;
using OnboardingApp.Api.Models;
using OnboardingApp.Api.Repositories;
using OnboardingApp.Api.Services;
using Xunit;

namespace OnboardingApp.Tests.Services;

public class OnboardingServiceTests
{
    private readonly IOnboardingRepository _repository = Substitute.For<IOnboardingRepository>();
    private readonly OnboardingService _service;

    public OnboardingServiceTests()
    {
        _service = new OnboardingService(_repository);
    }

    [Fact]
    public async Task GetTasksForDepartmentAsync_WithValidDepartmentId_ReturnsSortedTasks()
    {
        // Arrange
        var deptId = 1;
        var dept = new Department { Id = deptId, Name = "Engineering" };
        var tasks = new List<OnboardingTask>
        {
            new() { Id = 1, DepartmentId = deptId, Department = dept, Title = "Task A", TimelinePhase = TimelinePhase.WeekTwo, DisplayOrder = 2 },
            new() { Id = 2, DepartmentId = deptId, Department = dept, Title = "Task B", TimelinePhase = TimelinePhase.WeekOne, DisplayOrder = 1 },
            new() { Id = 3, DepartmentId = deptId, Department = dept, Title = "Task C", TimelinePhase = TimelinePhase.WeekOne, DisplayOrder = 2 }
        };

        _repository.GetDepartmentByIdAsync(deptId).Returns(dept);
        _repository.GetTasksByDepartmentIdAsync(deptId).Returns(tasks);

        // Act
        var result = await _service.GetTasksForDepartmentAsync(deptId, null);

        // Assert
        result.Should().HaveCount(3);
        var list = result.ToList();
        
        // Assert Ordering: TimelinePhase then DisplayOrder
        list[0].Id.Should().Be(2); // WeekOne, order 1
        list[1].Id.Should().Be(3); // WeekOne, order 2
        list[2].Id.Should().Be(1); // WeekTwo, order 2
    }

    [Fact]
    public async Task GetTasksForDepartmentAsync_WithValidRole_ReturnsSortedTasks()
    {
        // Arrange
        var deptId = 2;
        var role = "Sales";
        var dept = new Department { Id = deptId, Name = role };
        var tasks = new List<OnboardingTask>
        {
            new() { Id = 4, DepartmentId = deptId, Department = dept, Title = "Sales Task 1", TimelinePhase = TimelinePhase.WeekOne, DisplayOrder = 1 }
        };

        _repository.GetDepartmentByNameAsync(role).Returns(dept);
        _repository.GetTasksByDepartmentIdAsync(deptId).Returns(tasks);

        // Act
        var result = await _service.GetTasksForDepartmentAsync(null, role);

        // Assert
        result.Should().HaveCount(1);
        result.First().Id.Should().Be(4);
    }

    [Fact]
    public async Task GetTasksForDepartmentAsync_WithInvalidDepartmentId_ThrowsKeyNotFoundException()
    {
        // Arrange
        var deptId = 99;
        _repository.GetDepartmentByIdAsync(deptId).Returns((Department?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetTasksForDepartmentAsync(deptId, null));
    }

    [Fact]
    public async Task GetTasksForDepartmentAsync_WithInvalidRole_ThrowsKeyNotFoundException()
    {
        // Arrange
        var role = "UnknownRole";
        _repository.GetDepartmentByNameAsync(role).Returns((Department?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetTasksForDepartmentAsync(null, role));
    }

    [Fact]
    public async Task GetTasksForDepartmentAsync_WithNeitherIdNorRole_ThrowsArgumentException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(() => _service.GetTasksForDepartmentAsync(null, null));
    }

    [Fact]
    public async Task GetDepartmentsAsync_ReturnsMappedDepartments()
    {
        // Arrange
        var depts = new List<Department>
        {
            new() { Id = 1, Name = "Engineering", RoleKey = "engineering", Tagline = "Build things" },
            new() { Id = 2, Name = "Sales", RoleKey = "sales", Tagline = "Sell things" }
        };
        _repository.GetAllDepartmentsAsync().Returns(depts);

        // Act
        var result = await _service.GetDepartmentsAsync();

        // Assert
        result.Should().HaveCount(2);
        var list = result.ToList();
        list[0].Name.Should().Be("Engineering");
        list[1].Name.Should().Be("Sales");
    }

    [Fact]
    public async Task GetDepartmentDashboardAsync_WithValidRoleKey_ReturnsDashboardDto()
    {
        // Arrange
        var roleKey = "engineering";
        var dept = new Department
        {
            Id = 1,
            Name = "Engineering",
            RoleKey = roleKey,
            Tagline = "Build things",
            WelcomeMessage = "Welcome!",
            MondaySchedule = "office",
            TuesdaySchedule = "office",
            WednesdaySchedule = "office",
            ThursdaySchedule = "remote",
            FridaySchedule = "remote",
            Contacts = new List<DepartmentContact>
            {
                new() { Name = "Alice", Role = "Manager", AvatarInitials = "A", Email = "alice@meridian.com", Bio = "Manager Bio", SlackMemberId = "U123", GoogleMeetUrl = "http://meet", PreferredCommTool = "slack", DisplayOrder = 1 }
            }
        };

        _repository.GetDepartmentByRoleKeyAsync(roleKey).Returns(dept);

        // Act
        var result = await _service.GetDepartmentDashboardAsync(roleKey);

        // Assert
        result.DepartmentName.Should().Be("Engineering");
        result.RoleKey.Should().Be("engineering");
        result.Tagline.Should().Be("Build things");
        result.WelcomeMessage.Should().Be("Welcome!");
        result.WeeklySchedule.Should().HaveCount(5);
        result.Contacts.Should().HaveCount(1);
        result.Contacts.First().Name.Should().Be("Alice");
    }

    [Fact]
    public async Task GetDepartmentDashboardAsync_WithInvalidRoleKey_ThrowsKeyNotFoundException()
    {
        // Arrange
        var roleKey = "unknown";
        _repository.GetDepartmentByRoleKeyAsync(roleKey).Returns((Department?)null);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetDepartmentDashboardAsync(roleKey));
    }

    [Fact]
    public async Task GetTaskInstructionsAsync_WithValidTaskId_ReturnsTaskInstructionsDto()
    {
        // Arrange
        var taskId = 1;
        var task = new OnboardingTask { Id = taskId, Title = "Set up your IDE" };
        var instructions = new List<TaskInstruction>
        {
            new() { Id = 1, TaskId = taskId, StepNumber = 1, Text = "Download SDK" },
            new() { Id = 2, TaskId = taskId, StepNumber = 2, Text = "Run setup" }
        };

        _repository.GetTaskByIdAsync(taskId).Returns(task);
        _repository.GetTaskInstructionsByTaskIdAsync(taskId).Returns(instructions);

        // Act
        var result = await _service.GetTaskInstructionsAsync(taskId);

        // Assert
        result.Should().NotBeNull();
        result!.TaskId.Should().Be(taskId);
        result.TaskTitle.Should().Be("Set up your IDE");
        result.Steps.Should().HaveCount(2);
        result.Steps[0].Should().Be("Download SDK");
        result.Steps[1].Should().Be("Run setup");
    }

    [Fact]
    public async Task GetTaskInstructionsAsync_WithInvalidTaskId_ReturnsNull()
    {
        // Arrange
        var taskId = 99;
        _repository.GetTaskByIdAsync(taskId).Returns((OnboardingTask?)null);

        // Act
        var result = await _service.GetTaskInstructionsAsync(taskId);

        // Assert
        result.Should().BeNull();
    }
}
