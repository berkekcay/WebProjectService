using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Members;

public class UpdateMembershipStatusRequest
{
    public MembershipStatus MembershipStatus { get; set; }
}