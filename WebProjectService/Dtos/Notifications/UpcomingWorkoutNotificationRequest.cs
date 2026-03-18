namespace WebProjectService.Dtos.Notifications;

public class UpcomingWorkoutNotificationRequest
{
    public Guid MemberId { get; set; }
    public string WorkoutName { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
}