using System.ComponentModel.DataAnnotations;
using WebProjectService.Entities.Enums;

namespace WebProjectService.Dtos.Auth;

public class CreateStaffUserRequest
{
    [Required]
    [MinLength(3)]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    [MaxLength(128)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Phone]
    [MaxLength(32)]
    public string PhoneNumber { get; set; } = string.Empty;

    [EnumDataType(typeof(Role))]
    public Role Role { get; set; }

    [MaxLength(128)]
    public string? Specialization { get; set; }

    [MaxLength(500)]
    public string? CertificationDetails { get; set; }

    [MaxLength(1000)]
    public string? Bio { get; set; }

    [Range(0, 1000000000)]
    public decimal SalaryAmount { get; set; }
}