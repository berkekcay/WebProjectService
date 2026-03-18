using WebProjectService.Entities.Enums;

namespace WebProjectService.Entities;

public class Exercise : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public MuscleGroup MuscleGroup { get; set; }
    public string Description { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;

    public ICollection<WorkoutProgramExercise> WorkoutPrograms { get; set; } = new List<WorkoutProgramExercise>();
}