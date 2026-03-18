namespace WebProjectService.Dtos.Subscriptions;

public class AssignSubscriptionByTrainerRequest
{
    public int MemberId { get; set; }
    public int MembershipPlanId { get; set; }
    public bool IsPaid { get; set; }
}