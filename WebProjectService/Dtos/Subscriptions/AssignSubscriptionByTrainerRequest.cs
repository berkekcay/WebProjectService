using System.ComponentModel.DataAnnotations;

namespace WebProjectService.Dtos.Subscriptions;

public class AssignSubscriptionByTrainerRequest
{
    [Range(1, int.MaxValue)]
    public int MemberId { get; set; }

    [Range(1, int.MaxValue)]
    public int MembershipPlanId { get; set; }

    public bool IsPaid { get; set; }
}