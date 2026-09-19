namespace HiddenWing.BrandCenter.Domain.Entities;

/// <summary>
/// Product-level overrides for the global Hidden Wing brand.
/// Null fields inherit from the assigned or default theme and company brand.
/// </summary>
public sealed class ProductBranding : Entity
{
    public Guid ProductId { get; set; }

    public Guid? ThemeId { get; set; }

    public string? DisplayName { get; set; }

    public string? ShortName { get; set; }

    public string? Tagline { get; set; }

    public string? LogoUrl { get; set; }

    public string? LogoDarkUrl { get; set; }

    public string? LogoLightUrl { get; set; }

    public string? IconUrl { get; set; }

    public string? FaviconUrl { get; set; }

    public string? PrimaryColor { get; set; }

    public string? SecondaryColor { get; set; }

    public string? AccentColor { get; set; }

    public string? BackgroundColor { get; set; }

    public string? SurfaceColor { get; set; }

    public string? TextColor { get; set; }

    public string? MutedTextColor { get; set; }

    public string? BorderColor { get; set; }

    public string? FontFamily { get; set; }

    public string? SupportEmail { get; set; }

    public string? WebsiteUrl { get; set; }

    public string? PrivacyUrl { get; set; }

    public string? TermsUrl { get; set; }

    public bool IsEnabled { get; set; } = true;

    public Product Product { get; set; } = null!;

    public Theme? Theme { get; set; }
}
