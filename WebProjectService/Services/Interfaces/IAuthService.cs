using WebProjectService.Dtos.Members;
using WebProjectService.Services.Models;
using WebProjectService.Dtos.Auth;

namespace WebProjectService.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(MemberCreateRequest request, CancellationToken cancellationToken);
    Task<AuthResponse?> LoginAsync(string email, string password, CancellationToken cancellationToken);
    Task<AuthResponse> CreateStaffUserAsync(CreateStaffUserRequest request, CancellationToken cancellationToken);
}