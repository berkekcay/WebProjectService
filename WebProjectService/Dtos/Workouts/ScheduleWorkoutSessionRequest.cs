using System.ComponentModel.DataAnnotations;

namespace WebProjectService.Dtos.Workouts;

public class ScheduleWorkoutSessionRequest
{
    [Range(1, int.MaxValue)]
    public int MemberId { get; set; }

    [Range(1, int.MaxValue)]
    public int? TrainerId { get; set; }

    [Range(1, int.MaxValue)]
    public int? WorkoutProgramId { get; set; }

    [Required]
    public DateTime ScheduledDate { get; set; }

    [Range(1, 600)]
    public int DurationMinutes { get; set; }

    [MaxLength(1000)]
    public string Notes { get; set; } = string.Empty;
}