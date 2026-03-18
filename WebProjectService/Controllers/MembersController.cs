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
    [HttpGet("{memberId:guid}")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)},{nameof(Role.Member)}")]
    public async Task<IActionResult> GetById(Guid memberId, CancellationToken cancellationToken)
    {
        var member = await memberService.GetMemberAsync(memberId, cancellationToken);
        return member is null ? NotFound() : Ok(member);
    }

    [HttpPut("{memberId:guid}/freeze")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> FreezeMembership(Guid memberId, CancellationToken cancellationToken)
    {
        await memberService.FreezeMembershipAsync(memberId, cancellationToken);
        return NoContent();
    }

    [HttpPut("{memberId:guid}/status")]
    [Authorize(Roles = $"{nameof(Role.Admin)},{nameof(Role.Trainer)}")]
    public async Task<IActionResult> UpdateMembershipStatus(
        Guid memberId,
        [FromBody] UpdateMembershipStatusRequest request,
        CancellationToken cancellationToken)
    {
        await memberService.UpdateMembershipStatusAsync(memberId, request.MembershipStatus, cancellationToken);
        return NoContent();
    }
}