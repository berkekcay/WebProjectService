namespace WebProjectService.Services;

public class JwtOptions
{
    public const string SectionName = "Jwt";

    public string Key { get; set; } = "SoSecretKeyForWebProject12345";
    public string Issuer { get; set; } = "WebProjectService";
    public string Audience { get; set; } = "WebProjectServiceClients";
    public int ExpiresInMinutes { get; set; } = 120;
}