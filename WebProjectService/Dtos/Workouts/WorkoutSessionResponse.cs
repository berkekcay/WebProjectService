using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Workouts;

public class WorkoutSessionResponse
{
    public int Id { get; set; }
    public int MemberId { get; set; }
    public int? TrainerId { get; set; }
    public int? WorkoutProgramId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int DurationMinutes { get; set; }
    public string Notes { get; set; } = string.Empty;
    public WorkoutSessionStatus Status { get; set; }
}