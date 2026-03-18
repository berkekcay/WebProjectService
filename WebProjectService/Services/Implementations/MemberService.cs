using Microsoft.EntityFrameworkCore;
using WebProjectService.Data;
using WebProjectService.Dtos.Members;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Services.Implementations;

public class MemberService(AppDbContext context) : IMemberService
{
    public async Task<MemberResponse?> GetMemberAsync(Guid memberId, CancellationToken cancellationToken)
    {
        return await context.Members
            .AsNoTracking()
            .Where(x => x.Id == memberId)
            .Select(x => new MemberResponse
            {
                Name = x.User.Username,
                Status = x.MembershipStatus,
                PlanName = x.Subscriptions
                    .OrderByDescending(s => s.EndDate)
                    .Select(s => s.MembershipPlan.Title)
                    .FirstOrDefault() ?? "No Plan"
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task FreezeMembershipAsync(Guid memberId, CancellationToken cancellationToken)
    {
        await UpdateMembershipStatusAsync(memberId, MembershipStatus.Frozen, cancellationToken);
    }

    public async Task UpdateMembershipStatusAsync(Guid memberId, MembershipStatus membershipStatus, CancellationToken cancellationToken)
    {
        var member = await context.Members.FirstOrDefaultAsync(x => x.Id == memberId, cancellationToken)
            ?? throw new KeyNotFoundException("Member not found.");

        member.MembershipStatus = membershipStatus;
        await context.SaveChangesAsync(cancellationToken);
    }
}