using WebProjectService.Dtos.Members;
using WebProjectService.Entities.Enums;

namespace WebProjectService.Services.Interfaces;

public interface IMemberService
{
    Task<MemberResponse?> GetMemberAsync(int memberId, CancellationToken cancellationToken);
    Task FreezeMembershipAsync(int memberId, CancellationToken cancellationToken);
    Task UpdateMembershipStatusAsync(int memberId, MembershipStatus membershipStatus, CancellationToken cancellationToken);
}