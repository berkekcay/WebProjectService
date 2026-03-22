using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebProjectService.Dtos.Auth;
using WebProjectService.Dtos.Members;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;

namespace WebProjectService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] MemberCreateRequest request, CancellationToken cancellationToken)
    {
        var isRegistered = await authService.RegisterAsync(request, cancellationToken);
        return isRegistered
            ? Ok(new { success = true })
            : BadRequest(new { success = false });
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.LoginAsync(request.Email, request.Password, cancellationToken);
        return result is null ? Unauthorized() : Ok(result);
    }

    [HttpPost("staff")]
    [Authorize(Roles = nameof(Role.Admin))]
    public async Task<IActionResult> CreateStaff([FromBody] CreateStaffUserRequest request, CancellationToken cancellationToken)
    {
        var result = await authService.CreateStaffUserAsync(request, cancellationToken);
        return Ok(result);
    }
}