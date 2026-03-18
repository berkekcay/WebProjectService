using WebProjectService.Entities.Enums;

namespace WebProjectService.Entities;

public class WorkoutProgram : BaseEntity
{
    public int MemberId { get; set; }
    public int TrainerId { get; set; }
    public string ProgramName { get; set; } = string.Empty;
    public DifficultyLevel DifficultyLevel { get; set; } = DifficultyLevel.Beginner;

    public Member Member { get; set; } = null!;
    public Trainer Trainer { get; set; } = null!;
    public ICollection<WorkoutProgramExercise> ProgramExercises { get; set; } = new List<WorkoutProgramExercise>();
}