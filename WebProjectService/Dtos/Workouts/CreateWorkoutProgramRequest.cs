using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Workouts;

public class CreateWorkoutProgramRequest
{
    public Guid MemberId { get; set; }
    public Guid TrainerId { get; set; }
    public string ProgramName { get; set; } = string.Empty;
    public DifficultyLevel DifficultyLevel { get; set; }
    public IReadOnlyCollection<Guid> ExerciseIds { get; set; } = [];
}