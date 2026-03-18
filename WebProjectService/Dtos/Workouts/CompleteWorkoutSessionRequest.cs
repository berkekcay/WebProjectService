namespace WebProjectService.Dtos.Workouts;

public class CompleteWorkoutSessionRequest
{
    public int DurationMinutes { get; set; }
    public string Notes { get; set; } = string.Empty;
}