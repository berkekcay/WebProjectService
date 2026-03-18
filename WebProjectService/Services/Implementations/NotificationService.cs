using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebProjectService.Data;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Services.Implementations;

public class NotificationService(AppDbContext context, ILogger<NotificationService> logger) : INotificationService
{
    public async Task NotifyExpiredMembershipsAsync(CancellationToken cancellationToken)
    {
        var expiredMembers = await context.Members
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.MembershipStatus == MembershipStatus.Expired)
            .ToListAsync(cancellationToken);

        foreach (var member in expiredMembers)
        {
            logger.LogInformation("Sending expiration notification to {Email}", member.User.Email);
        }
    }

    public Task NotifyUpcomingWorkoutAsync(int memberId, string workoutName, DateTime scheduledDate, CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Sending workout reminder to member {MemberId} for {WorkoutName} at {ScheduledDate}",
            memberId,
            workoutName,
            scheduledDate);

        return Task.CompletedTask;
    }
}