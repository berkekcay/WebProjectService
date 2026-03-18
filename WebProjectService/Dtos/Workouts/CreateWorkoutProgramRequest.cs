using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Workouts;

public class CreateWorkoutProgramRequest
{
    public int MemberId { get; set; }
    public int TrainerId { get; set; }
    public string ProgramName { get; set; } = string.Empty;
    public DifficultyLevel DifficultyLevel { get; set; }
    public IReadOnlyCollection<int> ExerciseIds { get; set; } = [];
}