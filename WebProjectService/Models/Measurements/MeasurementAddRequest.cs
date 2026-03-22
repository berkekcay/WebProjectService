using System.ComponentModel.DataAnnotations;

namespace WebProjectService.Dtos.Measurements;

public class MeasurementAddRequest
{
    [Range(1, int.MaxValue)]
    public int MemberId { get; set; }

    [Range(typeof(decimal), "1", "1000")]
    public decimal Weight { get; set; }

    [Range(typeof(decimal), "30", "300")]
    public decimal Height { get; set; }

    [Range(typeof(decimal), "0", "100")]
    public decimal BodyFatPercentage { get; set; }

    [Range(typeof(decimal), "0", "500")]
    public decimal Chest { get; set; }

    [Range(typeof(decimal), "0", "500")]
    public decimal Waist { get; set; }

    [Range(typeof(decimal), "0", "500")]
    public decimal Arm { get; set; }

    [Range(typeof(decimal), "0", "500")]
    public decimal Leg { get; set; }

    [Required]
    public DateTime MeasurementDate { get; set; } = DateTime.UtcNow;
}