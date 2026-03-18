namespace WebProjectService.Dtos.Notifications;

public class UpcomingWorkoutNotificationRequest
{
    public int MemberId { get; set; }
    public string WorkoutName { get; set; } = string.Empty;
    public DateTime ScheduledDate { get; set; }
}