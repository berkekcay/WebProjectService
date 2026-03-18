using WebProjectService.Dtos.Members;
using WebProjectService.Entities.Enums;

namespace WebProjectService.Services.Interfaces;

public interface IMemberService
{
    Task<MemberResponse?> GetMemberAsync(Guid memberId, CancellationToken cancellationToken);
    Task FreezeMembershipAsync(Guid memberId, CancellationToken cancellationToken);
    Task UpdateMembershipStatusAsync(Guid memberId, MembershipStatus membershipStatus, CancellationToken cancellationToken);
}