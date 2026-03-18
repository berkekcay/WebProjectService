using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FinanceController(IFinanceService financeService) : ControllerBase
{
    [HttpGet("reports/total-revenue")]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<IActionResult> GetTotalRevenue(CancellationToken cancellationToken)
    {
        var totalRevenue = await financeService.GetTotalRevenueAsync(cancellationToken);
        return Ok(new { TotalRevenue = totalRevenue });
    }

    [HttpGet("reports/unpaid-subscriptions")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> GetUnpaidSubscriptions(CancellationToken cancellationToken)
    {
        var unpaidSubscriptions = await financeService.GetUnpaidSubscriptionsAsync(cancellationToken);
        return Ok(unpaidSubscriptions);
    }
}