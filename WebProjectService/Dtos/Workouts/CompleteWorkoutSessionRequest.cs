using System.ComponentModel.DataAnnotations;

namespace WebProjectService.Dtos.Workouts;

public class CompleteWorkoutSessionRequest
{
    [Range(1, 600)]
    public int DurationMinutes { get; set; }

    [MaxLength(1000)]
    public string Notes { get; set; } = string.Empty;
}