using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProjectService.Dtos.Members;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController(IMemberService memberService) : ControllerBase
{
    [HttpGet("{memberId:int}")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> GetById(int memberId, CancellationToken cancellationToken)
    {
        var member = await memberService.GetMemberAsync(memberId, cancellationToken);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpPut("{memberId:int}/freeze")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> FreezeMembership(int memberId, CancellationToken cancellationToken)
    {
        await memberService.FreezeMembershipAsync(memberId, cancellationToken);
        return NoContent();
    }

    [HttpPut("{memberId:int}/status")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> UpdateMembershipStatus(
        int memberId,
        [FromBody] UpdateMembershipStatusRequest request,
        CancellationToken cancellationToken)
    {
        await memberService.UpdateMembershipStatusAsync(memberId, request.MembershipStatus, cancellationToken);
        return NoContent();
    }
}