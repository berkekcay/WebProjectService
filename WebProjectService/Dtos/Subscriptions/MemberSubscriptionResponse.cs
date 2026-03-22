namespace WebProjectService.Dtos.Subscriptions;

public class MemberSubscriptionResponse
{
    public int SubscriptionId { get; set; }
    public int MemberId { get; set; }
    public int MembershipPlanId { get; set; }
    public string MembershipPlanTitle { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsPaid { get; set; }
}
