using Microsoft.AspNetCore.Mvc;
using OnboardingApp.Api.Controllers.Dtos;
using OnboardingApp.Api.Services;

namespace OnboardingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressController(IProgressService progressService) : ControllerBase
{
    [HttpPost("sync")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SyncProgress([FromBody] ProgressSyncRequest request)
    {
        if (!Guid.TryParse(request.SessionToken, out var token))
        {
            return BadRequest("Invalid session token format. A valid GUID is required.");
        }

        await progressService.SyncProgressAsync(token, request.CompletedTaskIds);
        return Ok();
    }

    [HttpGet("{token}")]
    [ProducesResponseType(typeof(IEnumerable<int>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProgress(string token)
    {
        if (!Guid.TryParse(token, out var sessionToken))
        {
            return BadRequest("Invalid session token format. A valid GUID is required.");
        }

        var completedTaskIds = await progressService.GetCompletedTaskIdsAsync(sessionToken);
        return Ok(completedTaskIds);
    }
}
