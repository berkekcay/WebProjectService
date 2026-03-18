namespace WebProjectService.Entities;

public class WorkoutProgramExercise
{
    public Guid WorkoutProgramId { get; set; }
    public Guid ExerciseId { get; set; }
    public int Order { get; set; }

    public WorkoutProgram WorkoutProgram { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
}