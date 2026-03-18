using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProjectService.Dtos.Notifications;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController(INotificationService notificationService) : ControllerBase
{
    [HttpPost("expired-memberships")]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<IActionResult> NotifyExpiredMemberships(CancellationToken cancellationToken)
    {
        await notificationService.NotifyExpiredMembershipsAsync(cancellationToken);
        return Ok(new { Message = "Expired membership notifications queued." });
    }

    [HttpPost("upcoming-workout")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> NotifyUpcomingWorkout(
        [FromBody] UpcomingWorkoutNotificationRequest request,
        CancellationToken cancellationToken)
    {
        await notificationService.NotifyUpcomingWorkoutAsync(
            request.MemberId,
            request.WorkoutName,
            request.ScheduledDate,
            cancellationToken);

        return Ok(new { Message = "Upcoming workout notification queued." });
    }
}