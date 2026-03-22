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
    [HttpGet("members/{memberId:int}/measurements")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> GetMeasurements(int memberId, CancellationToken cancellationToken)
    {
        var measurements = await progressService.GetMeasurementsAsync(memberId, cancellationToken);
        return Ok(measurements);
    }

    [HttpPost("measurements")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> AddMeasurement([FromBody] MeasurementAddRequest request, CancellationToken cancellationToken)
    {
        await progressService.AddMeasurementAsync(request, cancellationToken);
        return Created();
    }

    [HttpGet("members/{memberId:int}/chart")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> GetProgressChartData(int memberId, CancellationToken cancellationToken)
    {
        var data = await progressService.GetProgressChartDataAsync(memberId, cancellationToken);
        return Ok(data);
    }
}