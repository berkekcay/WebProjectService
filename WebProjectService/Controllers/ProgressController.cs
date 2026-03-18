using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProjectService.Dtos.Measurements;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProgressController(IProgressService progressService) : ControllerBase
{
    [HttpPost("measurements")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> AddMeasurement([FromBody] MeasurementAddRequest request, CancellationToken cancellationToken)
    {
        await progressService.AddMeasurementAsync(request, cancellationToken);
        return Created();
    }

    [HttpGet("members/{memberId:guid}/chart")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> GetProgressChartData(Guid memberId, CancellationToken cancellationToken)
    {
        var data = await progressService.GetProgressChartDataAsync(memberId, cancellationToken);
        return Ok(data);
    }
}