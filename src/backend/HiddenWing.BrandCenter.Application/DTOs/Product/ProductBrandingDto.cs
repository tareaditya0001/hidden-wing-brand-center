namespace HiddenWing.BrandCenter.Application.DTOs.Product;

public sealed class ProductBrandingDto
{
    public Guid Id { get; init; }

    public Guid ProductId { get; init; }

    public Guid? ThemeId { get; init; }

    public string? DisplayName { get; init; }

    public string? ShortName { get; init; }

    public string? Tagline { get; init; }

    public string? LogoUrl { get; init; }

    public string? LogoDarkUrl { get; init; }

    public string? LogoLightUrl { get; init; }

    public string? IconUrl { get; init; }

    public string? FaviconUrl { get; init; }

    public string? PrimaryColor { get; init; }

    public string? SecondaryColor { get; init; }

    public string? AccentColor { get; init; }

    public string? BackgroundColor { get; init; }

    public string? SurfaceColor { get; init; }

    public string? TextColor { get; init; }

    public string? MutedTextColor { get; init; }

    public string? BorderColor { get; init; }

    public string? FontFamily { get; init; }

    public string? SupportEmail { get; init; }

    public string? WebsiteUrl { get; init; }

    public string? PrivacyUrl { get; init; }

    public string? TermsUrl { get; init; }

    public bool IsEnabled { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; init; }
}
