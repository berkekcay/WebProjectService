using Microsoft.EntityFrameworkCore;
using WebProjectService.Data;
using WebProjectService.Dtos.Subscriptions;
using WebProjectService.Entities;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Services.Implementations;

public class SubscriptionService(AppDbContext context) : ISubscriptionService
{
    public async Task<int> DeactivateExpiredSubscriptionsAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;

        var expiredSubscriptions = await context.Subscriptions
            .Include(x => x.Member)
            .Where(x => x.EndDate < now && x.Member.MembershipStatus == MembershipStatus.Active)
            .ToListAsync(cancellationToken);

        foreach (var subscription in expiredSubscriptions)
        {
            subscription.Member.MembershipStatus = MembershipStatus.Expired;
        }

        await context.SaveChangesAsync(cancellationToken);
        return expiredSubscriptions.Count;
    }

    public async Task RenewSubscriptionAsync(int memberId, int membershipPlanId, CancellationToken cancellationToken)
    {
        await CreateOrRenewSubscriptionAsync(memberId, membershipPlanId, false, cancellationToken);
    }

    public async Task<IReadOnlyCollection<MembershipPlanResponse>> GetMembershipPlansAsync(CancellationToken cancellationToken)
    {
        return await context.MembershipPlans
            .AsNoTracking()
            .Where(x => x.IsActive)
            .OrderBy(x => x.MonthlyPrice)
            .Select(x => new MembershipPlanResponse
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                MonthlyPrice = x.MonthlyPrice,
                DurationInMonths = x.DurationInMonths,
                CanFreeze = x.CanFreeze
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<MembershipPlanResponse> CreateMembershipPlanAsync(MembershipPlanCreateRequest request, CancellationToken cancellationToken)
    {
        var plan = new MembershipPlan
        {
            Title = request.Title,
            Description = request.Description,
            MonthlyPrice = request.MonthlyPrice,
            DurationInMonths = request.DurationInMonths,
            CanFreeze = request.CanFreeze
        };

        context.MembershipPlans.Add(plan);
        await context.SaveChangesAsync(cancellationToken);

        return new MembershipPlanResponse
        {
            Id = plan.Id,
            Title = plan.Title,
            Description = plan.Description,
            MonthlyPrice = plan.MonthlyPrice,
            DurationInMonths = plan.DurationInMonths,
            CanFreeze = plan.CanFreeze
        };
    }

    public async Task RenewSubscriptionByUserIdAsync(int userId, int membershipPlanId, bool isPaid, CancellationToken cancellationToken)
    {
        var member = await context.Members.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken)
            ?? throw new KeyNotFoundException("Member profile not found for user.");

        await CreateOrRenewSubscriptionAsync(member.Id, membershipPlanId, isPaid, cancellationToken);
    }

    public async Task AssignSubscriptionByTrainerAsync(int trainerUserId, int memberId, int membershipPlanId, bool isPaid, CancellationToken cancellationToken)
    {
        var trainerExists = await context.Trainers.AsNoTracking().AnyAsync(x => x.UserId == trainerUserId, cancellationToken);
        if (!trainerExists)
        {
            throw new InvalidOperationException("Trainer profile not found for current user.");
        }

        await CreateOrRenewSubscriptionAsync(memberId, membershipPlanId, isPaid, cancellationToken);
    }

    private async Task CreateOrRenewSubscriptionAsync(int memberId, int membershipPlanId, bool isPaid, CancellationToken cancellationToken)
    {
        var plan = await context.MembershipPlans.FirstOrDefaultAsync(x => x.Id == membershipPlanId, cancellationToken)
            ?? throw new KeyNotFoundException("Membership plan not found.");

        var subscription = new Subscription
        {
            MemberId = memberId,
            MembershipPlanId = membershipPlanId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(plan.DurationInMonths),
            IsPaid = isPaid
        };

        context.Subscriptions.Add(subscription);

        var member = await context.Members.FirstOrDefaultAsync(x => x.Id == memberId, cancellationToken)
            ?? throw new KeyNotFoundException("Member not found.");
        member.MembershipStatus = MembershipStatus.Active;

        await context.SaveChangesAsync(cancellationToken);
    }
}