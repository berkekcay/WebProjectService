namespace WebProjectService.Entities;

public class Subscription : BaseEntity
{
    public Guid MemberId { get; set; }
    public Guid MembershipPlanId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsPaid { get; set; }

    public Member Member { get; set; } = null!;
    public MembershipPlan MembershipPlan { get; set; } = null!;
}