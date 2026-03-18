namespace WebProjectService.Dtos.Subscriptions;

public class AssignSubscriptionByTrainerRequest
{
    public Guid MemberId { get; set; }
    public Guid MembershipPlanId { get; set; }
    public bool IsPaid { get; set; }
}