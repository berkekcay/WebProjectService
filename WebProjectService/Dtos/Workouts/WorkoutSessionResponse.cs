using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Workouts;

public class WorkoutSessionResponse
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }
    public Guid? TrainerId { get; set; }
    public Guid? WorkoutProgramId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int DurationMinutes { get; set; }
    public string Notes { get; set; } = string.Empty;
    public WorkoutSessionStatus Status { get; set; }
}