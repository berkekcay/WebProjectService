namespace WebProjectService.Dtos.Subscriptions;

public class RenewSubscriptionRequest
{
    public int MemberId { get; set; }
    public int MembershipPlanId { get; set; }
}