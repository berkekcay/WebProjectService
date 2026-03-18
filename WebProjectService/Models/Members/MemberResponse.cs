using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Members;

public class MemberResponse
{
    public string Name { get; set; } = string.Empty;
    public MembershipStatus Status { get; set; }
    public string PlanName { get; set; } = string.Empty;
}