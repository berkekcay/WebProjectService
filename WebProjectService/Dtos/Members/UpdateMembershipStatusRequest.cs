using System.ComponentModel.DataAnnotations;
using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Members;

public class UpdateMembershipStatusRequest
{
    [EnumDataType(typeof(MembershipStatus))]
    public MembershipStatus MembershipStatus { get; set; }
}