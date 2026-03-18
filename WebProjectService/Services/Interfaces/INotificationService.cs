namespace WebProjectService.Services.Interfaces;

public interface INotificationService
{
    Task NotifyExpiredMembershipsAsync(CancellationToken cancellationToken);
    Task NotifyUpcomingWorkoutAsync(Guid memberId, string workoutName, DateTime scheduledDate, CancellationToken cancellationToken);
}