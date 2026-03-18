namespace WebProjectService.Dtos.Subscriptions;

public class MembershipPlanResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal MonthlyPrice { get; set; }
    public int DurationInMonths { get; set; }
    public bool CanFreeze { get; set; }
}