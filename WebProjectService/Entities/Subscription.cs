namespace WebProjectService.Entities;

public class Subscription : BaseEntity
{
    public int MemberId { get; set; }
    public int MembershipPlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsPaid { get; set; }

    public Member Member { get; set; } = null!;
    public MembershipPlan MembershipPlan { get; set; } = null!;
}