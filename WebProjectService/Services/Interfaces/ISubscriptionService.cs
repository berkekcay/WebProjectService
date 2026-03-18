namespace WebProjectService.Services.Interfaces;

public interface ISubscriptionService
{
    Task<int> DeactivateExpiredSubscriptionsAsync(CancellationToken cancellationToken);
    Task RenewSubscriptionAsync(int memberId, int membershipPlanId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<WebProjectService.Dtos.Subscriptions.MembershipPlanResponse>> GetMembershipPlansAsync(CancellationToken cancellationToken);
    Task<WebProjectService.Dtos.Subscriptions.MembershipPlanResponse> CreateMembershipPlanAsync(WebProjectService.Dtos.Subscriptions.MembershipPlanCreateRequest request, CancellationToken cancellationToken);
    Task RenewSubscriptionByUserIdAsync(int userId, int membershipPlanId, bool isPaid, CancellationToken cancellationToken);
    Task AssignSubscriptionByTrainerAsync(int trainerUserId, int memberId, int membershipPlanId, bool isPaid, CancellationToken cancellationToken);
}