namespace WebProjectService.Dtos.Subscriptions;

public class RenewSubscriptionRequest
{
    public Guid MemberId { get; set; }
    public Guid MembershipPlanId { get; set; }
}