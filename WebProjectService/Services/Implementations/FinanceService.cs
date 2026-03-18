using Microsoft.EntityFrameworkCore;
using WebProjectService.Data;
using WebProjectService.Entities;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Services.Implementations;

public class FinanceService(AppDbContext context) : IFinanceService
{
    public async Task<decimal> GetTotalRevenueAsync(CancellationToken cancellationToken)
    {
        return await context.Subscriptions
            .AsNoTracking()
            .Where(x => x.IsPaid)
            .SumAsync(x => x.MembershipPlan.MonthlyPrice * x.MembershipPlan.DurationInMonths, cancellationToken);
    }

    public async Task<IReadOnlyCollection<Subscription>> GetUnpaidSubscriptionsAsync(CancellationToken cancellationToken)
    {
        return await context.Subscriptions
            .AsNoTracking()
            .Include(x => x.Member)
            .Include(x => x.MembershipPlan)
            .Where(x => !x.IsPaid)
            .OrderBy(x => x.EndDate)
            .ToListAsync(cancellationToken);
    }
}