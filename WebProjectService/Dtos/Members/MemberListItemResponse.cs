using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Members;

public class MemberListItemResponse
{
    public int MemberId { get; set; }
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public MembershipStatus Status { get; set; }
    public string PlanName { get; set; } = string.Empty;
}
