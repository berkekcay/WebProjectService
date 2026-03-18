namespace WebProjectService.Services.Interfaces;

public interface ISubscriptionService
{
    Task<int> DeactivateExpiredSubscriptionsAsync(CancellationToken cancellationToken);
    Task RenewSubscriptionAsync(Guid memberId, Guid membershipPlanId, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<WebProjectService.Dtos.Subscriptions.MembershipPlanResponse>> GetMembershipPlansAsync(CancellationToken cancellationToken);
    Task<WebProjectService.Dtos.Subscriptions.MembershipPlanResponse> CreateMembershipPlanAsync(WebProjectService.Dtos.Subscriptions.MembershipPlanCreateRequest request, CancellationToken cancellationToken);
    Task RenewSubscriptionByUserIdAsync(Guid userId, Guid membershipPlanId, bool isPaid, CancellationToken cancellationToken);
    Task AssignSubscriptionByTrainerAsync(Guid trainerUserId, Guid memberId, Guid membershipPlanId, bool isPaid, CancellationToken cancellationToken);
}