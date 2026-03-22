using System.ComponentModel.DataAnnotations;

namespace WebProjectService.Dtos.Subscriptions;

public class SelectMembershipPlanRequest
{
    [Range(1, int.MaxValue)]
    public int MembershipPlanId { get; set; }
}