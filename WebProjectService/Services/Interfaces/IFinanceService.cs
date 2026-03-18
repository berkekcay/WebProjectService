using WebProjectService.Entities;

namespace WebProjectService.Services.Interfaces;

public interface IFinanceService
{
    Task<decimal> GetTotalRevenueAsync(CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Subscription>> GetUnpaidSubscriptionsAsync(CancellationToken cancellationToken);
}