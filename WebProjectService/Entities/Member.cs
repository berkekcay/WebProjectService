using WebProjectService.Entities.Enums;

namespace WebProjectService.Entities;

public class Member : BaseEntity
{
    public int UserId { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; } = Gender.Unspecified;
    public string EmergencyContactName { get; set; } = string.Empty;
    public string EmergencyContactPhone { get; set; } = string.Empty;
    public string BloodType { get; set; } = string.Empty;
    public MembershipStatus MembershipStatus { get; set; } = MembershipStatus.Active;

    public User User { get; set; } = null!;
    public ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();
    public ICollection<WorkoutProgram> WorkoutPrograms { get; set; } = new List<WorkoutProgram>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}