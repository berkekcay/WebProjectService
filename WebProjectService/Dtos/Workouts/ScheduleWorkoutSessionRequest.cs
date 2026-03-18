namespace WebProjectService.Dtos.Workouts;

public class ScheduleWorkoutSessionRequest
{
    public int MemberId { get; set; }
    public int? TrainerId { get; set; }
    public int? WorkoutProgramId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public int DurationMinutes { get; set; }
    public string Notes { get; set; } = string.Empty;
}