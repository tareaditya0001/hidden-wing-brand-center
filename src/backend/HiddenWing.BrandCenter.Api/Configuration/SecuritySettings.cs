namespace HiddenWing.BrandCenter.Api.Configuration;

public sealed class SecuritySettings
{
    public const string SectionName = "Security";

    public string JwtIssuer { get; set; } = "HiddenWing.BrandCenter";

    public string JwtAudience { get; set; } = "HiddenWing.BrandCenter";

    public string JwtSecret { get; set; } = string.Empty;

    public int TokenLifetimeMinutes { get; set; } = 480;

    public string AdminUsername { get; set; } = string.Empty;

    public string AdminPassword { get; set; } = string.Empty;
}
