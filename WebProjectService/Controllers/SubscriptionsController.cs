using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProjectService.Extensions;
using WebProjectService.Dtos.Subscriptions;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController(ISubscriptionService subscriptionService) : ControllerBase
{
    [HttpGet("plans")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPlans(CancellationToken cancellationToken)
    {
        var plans = await subscriptionService.GetMembershipPlansAsync(cancellationToken);
        return Ok(plans);
    }

    [HttpPost("plans")]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<IActionResult> CreatePlan([FromBody] MembershipPlanCreateRequest request, CancellationToken cancellationToken)
    {
        var plan = await subscriptionService.CreateMembershipPlanAsync(request, cancellationToken);
        return Ok(plan);
    }

    [HttpPost("select-plan")]
    [Authorize(Roles = nameof(Role.Member))]
    public async Task<IActionResult> SelectPlan([FromBody] SelectMembershipPlanRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetRequiredUserId();
        await subscriptionService.RenewSubscriptionByUserIdAsync(userId, request.MembershipPlanId, false, cancellationToken);
        return NoContent();
    }

    [HttpPost("assign-by-trainer")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> AssignByTrainer([FromBody] AssignSubscriptionByTrainerRequest request, CancellationToken cancellationToken)
    {
        var trainerUserId = User.GetRequiredUserId();
        await subscriptionService.AssignSubscriptionByTrainerAsync(
            trainerUserId,
            request.MemberId,
            request.MembershipPlanId,
            request.IsPaid,
            cancellationToken);

        return NoContent();
    }

    [HttpPost("deactivate-expired")]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<IActionResult> DeactivateExpired(CancellationToken cancellationToken)
    {
        var count = await subscriptionService.DeactivateExpiredSubscriptionsAsync(cancellationToken);
        return Ok(new { DeactivatedMemberCount = count });
    }

    [HttpPost("renew")]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<IActionResult> Renew([FromBody] RenewSubscriptionRequest request, CancellationToken cancellationToken)
    {
        await subscriptionService.RenewSubscriptionAsync(request.MemberId, request.MembershipPlanId, cancellationToken);
        return NoContent();
    }
}