using System.Security.Claims;

namespace WebProjectService.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetRequiredUserId(this ClaimsPrincipal user)
    {
        var claimValue = user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? user.FindFirstValue(ClaimTypes.Name)
            ?? user.FindFirstValue(ClaimTypes.Email)
            ?? user.FindFirstValue("sub");

        if (Guid.TryParse(claimValue, out var userId))
        {
            return userId;
        }

        throw new UnauthorizedAccessException("User id claim not found.");
    }
}