using Microsoft.AspNetCore.Mvc;
using OnboardingApp.Api.Controllers.Dtos;
using OnboardingApp.Api.Services;

namespace OnboardingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController(IOnboardingService onboardingService) : ControllerBase
{
    /// <summary>
    /// Returns a lightweight list of all departments (id, name, roleKey, tagline).
    /// Used by the client to populate the department selector on first load.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DepartmentDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await onboardingService.GetDepartmentsAsync();
        return Ok(departments);
    }

    /// <summary>
    /// Returns the full dashboard payload for a given department role key,
    /// including the weekly hybrid schedule and the Who's Who contact directory.
    /// </summary>
    [HttpGet("{roleKey}/dashboard")]
    [ProducesResponseType(typeof(DashboardDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDashboard(string roleKey)
    {
        try
        {
            var dashboard = await onboardingService.GetDepartmentDashboardAsync(roleKey);
            return Ok(dashboard);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }
}
