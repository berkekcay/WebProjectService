using System.ComponentModel.DataAnnotations;

namespace WebProjectService.Dtos.Notifications;

public class UpcomingWorkoutNotificationRequest
{
    [Range(1, int.MaxValue)]
    public int MemberId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string WorkoutName { get; set; } = string.Empty;

    [Required]
    public DateTime ScheduledDate { get; set; }
}