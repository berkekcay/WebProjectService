using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Auth;

public class CreateStaffUserRequest
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Role Role { get; set; }
    public string? Specialization { get; set; }
    public string? CertificationDetails { get; set; }
    public string? Bio { get; set; }
    public decimal SalaryAmount { get; set; }
}