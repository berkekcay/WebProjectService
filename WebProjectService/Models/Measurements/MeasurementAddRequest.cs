namespace WebProjectService.Dtos.Measurements;

public class MeasurementAddRequest
{
    public Guid MemberId { get; set; }
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public decimal BodyFatPercentage { get; set; }
    public decimal Chest { get; set; }
    public decimal Waist { get; set; }
    public decimal Arm { get; set; }
    public decimal Leg { get; set; }
    public DateTime MeasurementDate { get; set; } = DateTime.UtcNow;
}