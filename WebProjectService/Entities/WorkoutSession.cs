using WebProjectService.Entities.Enums;

namespace WebProjectService.Entities;

public class WorkoutSession : BaseEntity
{
    public Guid MemberId { get; set; }
    public Guid? TrainerId { get; set; }
    public Guid? WorkoutProgramId { get; set; }
    public DateTime ScheduledDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    public int DurationMinutes { get; set; }
    public string Notes { get; set; } = string.Empty;
    public WorkoutSessionStatus Status { get; set; } = WorkoutSessionStatus.Scheduled;

    public Member Member { get; set; } = null!;
    public Trainer? Trainer { get; set; }
    public WorkoutProgram? WorkoutProgram { get; set; }
}