using System.ComponentModel.DataAnnotations;

namespace WebProjectService.Dtos.Subscriptions;

public class MembershipPlanCreateRequest
{
    [Required]
    [MinLength(2)]
    [MaxLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MinLength(5)]
    [MaxLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Range(typeof(decimal), "0.01", "1000000000")]
    public decimal MonthlyPrice { get; set; }

    [Range(1, 120)]
    public int DurationInMonths { get; set; }

    public bool CanFreeze { get; set; }
}