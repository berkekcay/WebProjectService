using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebProjectService.Data;
using WebProjectService.Dtos.Auth;
using WebProjectService.Dtos.Members;
using WebProjectService.Entities;
using WebProjectService.Entities.Enums;
using WebProjectService.Services.Interfaces;
using WebProjectService.Services.Models;

namespace WebProjectService.Services.Implementations;

public class AuthService(AppDbContext context, IOptions<JwtOptions> jwtOptions) : IAuthService
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;

    public async Task<AuthResponse> RegisterAsync(MemberCreateRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await context.Users.AsNoTracking().AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (existingUser)
        {
            throw new InvalidOperationException("Email is already in use.");
        }

        var user = new User
        {
            Username = request.Name,
            Email = request.Email,
            PasswordHash = ComputeSha256Hash(request.Password),
            Role = Role.Member,
            PhoneNumber = string.Empty
        };

        var member = new Member
        {
            User = user,
            DateOfBirth = DateTime.UtcNow,
            Gender = Gender.Unspecified,
            EmergencyContactName = string.Empty,
            EmergencyContactPhone = string.Empty,
            BloodType = string.Empty,
            MembershipStatus = MembershipStatus.Active
        };

        context.Users.Add(user);
        context.Members.Add(member);
        await context.SaveChangesAsync(cancellationToken);

        return CreateToken(user, member.Id, null);
    }

    public async Task<AuthResponse?> LoginAsync(string email, string password, CancellationToken cancellationToken)
    {
        var passwordHash = ComputeSha256Hash(password);
        var user = await context.Users
            .Include(x => x.MemberProfile)
            .Include(x => x.TrainerProfile)
            .FirstOrDefaultAsync(
            x => x.Email == email && x.PasswordHash == passwordHash,
            cancellationToken);

        if (user is null)
        {
            return null;
        }

        user.LastLoginDate = DateTime.UtcNow;
        await context.SaveChangesAsync(cancellationToken);

        return CreateToken(user, user.MemberProfile?.Id, user.TrainerProfile?.Id);
    }

    public async Task<AuthResponse> CreateStaffUserAsync(CreateStaffUserRequest request, CancellationToken cancellationToken)
    {
        if (request.Role is not Role.Admin and not Role.Trainer)
        {
            throw new InvalidOperationException("Only Admin or Trainer users can be created via this endpoint.");
        }

        var existingUser = await context.Users.AsNoTracking().AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (existingUser)
        {
            throw new InvalidOperationException("Email is already in use.");
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = ComputeSha256Hash(request.Password),
            Role = request.Role,
            PhoneNumber = request.PhoneNumber
        };

        context.Users.Add(user);

        Guid? trainerId = null;
        if (request.Role == Role.Trainer)
        {
            var trainer = new Trainer
            {
                User = user,
                Specialization = request.Specialization ?? string.Empty,
                CertificationDetails = request.CertificationDetails ?? string.Empty,
                Bio = request.Bio ?? string.Empty,
                SalaryAmount = request.SalaryAmount
            };

            context.Trainers.Add(trainer);
            trainerId = trainer.Id;
        }

        await context.SaveChangesAsync(cancellationToken);
        return CreateToken(user, null, trainerId);
    }

    private AuthResponse CreateToken(User user, Guid? memberId, Guid? trainerId)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new AuthResponse
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expiresAt,
            UserId = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role.ToString(),
            MemberId = memberId,
            TrainerId = trainerId
        };
    }

    private static string ComputeSha256Hash(string input)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes);
    }
}