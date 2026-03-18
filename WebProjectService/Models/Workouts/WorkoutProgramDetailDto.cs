using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Workouts;

public class WorkoutProgramDetailDto
{
    public Guid ProgramId { get; set; }
    public Guid MemberId { get; set; }
    public Guid TrainerId { get; set; }
    public string ProgramName { get; set; } = string.Empty;
    public DifficultyLevel DifficultyLevel { get; set; }
    public IReadOnlyCollection<ExerciseDto> Exercises { get; set; } = [];
}