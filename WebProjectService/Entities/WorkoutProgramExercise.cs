namespace WebProjectService.Entities;

public class WorkoutProgramExercise
{
    public int WorkoutProgramId { get; set; }
    public int ExerciseId { get; set; }
    public int Order { get; set; }

    public WorkoutProgram WorkoutProgram { get; set; } = null!;
    public Exercise Exercise { get; set; } = null!;
}