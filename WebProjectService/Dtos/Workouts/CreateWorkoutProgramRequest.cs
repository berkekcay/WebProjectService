using System.ComponentModel.DataAnnotations;
using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Workouts;

public class CreateWorkoutProgramRequest
{
    [Range(1, int.MaxValue)]
    public int MemberId { get; set; }

    [Range(1, int.MaxValue)]
    public int TrainerId { get; set; }

    [Required]
    [MinLength(2)]
    [MaxLength(120)]
    public string ProgramName { get; set; } = string.Empty;

    [EnumDataType(typeof(DifficultyLevel))]
    public DifficultyLevel DifficultyLevel { get; set; }

    [Required]
    [MinLength(1)]
    public IReadOnlyCollection<int> ExerciseIds { get; set; } = [];
}