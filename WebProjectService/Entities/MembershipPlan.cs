namespace WebProjectService.Entities;

public class MembershipPlan : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal MonthlyPrice { get; set; }
    public int DurationInMonths { get; set; }
    public bool CanFreeze { get; set; }

    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}