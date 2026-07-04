using Microsoft.AspNetCore.Mvc;
using OnboardingApp.Api.Controllers.Dtos;
using OnboardingApp.Api.Services;

namespace OnboardingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController(IOnboardingService onboardingService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTasks([FromQuery] int? departmentId, [FromQuery] string? role)
    {
        try
        {
            var tasks = await onboardingService.GetTasksForDepartmentAsync(departmentId, role);
            return Ok(tasks);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
