using WebProjectService.Entities.Enums;

namespace WebProjectService.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Member;
    public DateTime? LastLoginDate { get; set; }

    public Member? MemberProfile { get; set; }
    public Trainer? TrainerProfile { get; set; }
}