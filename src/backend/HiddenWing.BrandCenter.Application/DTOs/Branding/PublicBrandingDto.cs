namespace HiddenWing.BrandCenter.Application.DTOs.Branding;

/// <summary>
/// Resolved branding payload consumed by other Hidden Wing applications.
/// </summary>
public sealed class PublicBrandingDto
{
    public required CompanyBrandingDto Company { get; init; }

    public required ProductBrandingPublicDto Product { get; init; }

    public required ThemeBrandingPublicDto Theme { get; init; }

    public required ContactBrandingDto Contact { get; init; }
}

public sealed class CompanyBrandingDto
{
    public required string Name { get; init; }

    public string? Logo { get; init; }
}

public sealed class ProductBrandingPublicDto
{
    public required string Name { get; init; }

    public required string Slug { get; init; }

    public string? ShortName { get; init; }

    public string? Tagline { get; init; }

    public string? Logo { get; init; }

    public string? LogoDark { get; init; }

    public string? LogoLight { get; init; }

    public string? Icon { get; init; }

    public string? Favicon { get; init; }
}

public sealed class ThemeBrandingPublicDto
{
    public required string PrimaryColor { get; init; }

    public required string SecondaryColor { get; init; }

    public required string AccentColor { get; init; }

    public required string BackgroundColor { get; init; }

    public required string SurfaceColor { get; init; }

    public required string TextColor { get; init; }

    public required string MutedTextColor { get; init; }

    public required string BorderColor { get; init; }

    public required string FontFamily { get; init; }

    public required string BorderRadius { get; init; }
}

public sealed class ContactBrandingDto
{
    public string? SupportEmail { get; init; }

    public string? WebsiteUrl { get; init; }

    public string? PrivacyUrl { get; init; }

    public string? TermsUrl { get; init; }
}
