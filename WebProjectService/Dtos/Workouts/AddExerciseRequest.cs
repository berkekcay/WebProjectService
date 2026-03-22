using System.ComponentModel.DataAnnotations;
using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Workouts;

public class AddExerciseRequest
{
    [Required]
    [MinLength(2)]
    [MaxLength(120)]
    public string Name { get; set; } = string.Empty;

    [EnumDataType(typeof(MuscleGroup))]
    public MuscleGroup MuscleGroup { get; set; }

    [Required]
    [MinLength(5)]
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(500)]
    public string VideoUrl { get; set; } = string.Empty;
}