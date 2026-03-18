using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Workouts;

public class ExerciseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public MuscleGroup MuscleGroup { get; set; }
    public string Description { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
}