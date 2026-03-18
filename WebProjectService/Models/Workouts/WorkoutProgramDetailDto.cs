using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Workouts;

public class WorkoutProgramDetailDto
{
    public int ProgramId { get; set; }
    public int MemberId { get; set; }
    public int TrainerId { get; set; }
    public string ProgramName { get; set; } = string.Empty;
    public DifficultyLevel DifficultyLevel { get; set; }
    public IReadOnlyCollection<ExerciseDto> Exercises { get; set; } = [];
}