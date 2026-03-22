using System.ComponentModel.DataAnnotations;

namespace WebProjectService.Dtos.Members;

public class MemberCreateRequest
{
    [Required]
    [EmailAddress]
    [MaxLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    [MaxLength(128)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [MinLength(2)]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}