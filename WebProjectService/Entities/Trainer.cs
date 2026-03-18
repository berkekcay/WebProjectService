namespace WebProjectService.Entities;

public class Trainer : BaseEntity
{
    public int UserId { get; set; }
    public string Specialization { get; set; } = string.Empty;
    public string CertificationDetails { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public decimal SalaryAmount { get; set; }

    public User User { get; set; } = null!;
    public ICollection<WorkoutProgram> WorkoutPrograms { get; set; } = new List<WorkoutProgram>();
}