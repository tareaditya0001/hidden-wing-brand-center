using HiddenWing.BrandCenter.Application.DTOs.Branding;
using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Domain.Enums;

namespace HiddenWing.BrandCenter.Application.Branding;

/// <summary>
/// Applies Global Brand → Theme → Product Branding override inheritance.
/// The first non-empty value wins.
/// </summary>
public static class BrandingResolver
{
    public static PublicBrandingDto Resolve(
        Brand brand,
        Product product,
        ProductBranding? branding,
        Theme? assignedTheme,
        Theme defaultTheme,
        IReadOnlyCollection<BrandAsset> assets)
    {
        var theme = assignedTheme ?? defaultTheme;
        var companyLogo = FindAsset(assets, AssetType.Logo, productId: null);
        var productLogo = FindAsset(assets, AssetType.Logo, product.Id) ?? companyLogo;
        var productLogoDark = FindAsset(assets, AssetType.LogoDark, product.Id);
        var productLogoLight = FindAsset(assets, AssetType.LogoLight, product.Id);
        var productIcon = FindAsset(assets, AssetType.Icon, product.Id);
        var productFavicon = FindAsset(assets, AssetType.Favicon, product.Id);

        return new PublicBrandingDto
        {
            Company = new CompanyBrandingDto
            {
                Name = brand.CompanyName,
                Logo = companyLogo?.Url
            },
            Product = new ProductBrandingPublicDto
            {
                Name = First(branding?.DisplayName, product.Name),
                Slug = product.Slug,
                ShortName = branding?.ShortName,
                Tagline = branding?.Tagline,
                Logo = First(branding?.LogoUrl, productLogo?.Url),
                LogoDark = First(branding?.LogoDarkUrl, productLogoDark?.Url, productLogo?.Url),
                LogoLight = First(branding?.LogoLightUrl, productLogoLight?.Url, productLogo?.Url),
                Icon = First(branding?.IconUrl, productIcon?.Url),
                Favicon = First(branding?.FaviconUrl, productFavicon?.Url)
            },
            Theme = new ThemeBrandingPublicDto
            {
                PrimaryColor = First(branding?.PrimaryColor, theme.PrimaryColor),
                SecondaryColor = First(branding?.SecondaryColor, theme.SecondaryColor),
                AccentColor = First(branding?.AccentColor, theme.AccentColor),
                BackgroundColor = First(branding?.BackgroundColor, theme.BackgroundColor),
                SurfaceColor = First(branding?.SurfaceColor, theme.SurfaceColor),
                TextColor = First(branding?.TextColor, theme.TextColor),
                MutedTextColor = First(branding?.MutedTextColor, theme.MutedTextColor),
                BorderColor = First(branding?.BorderColor, theme.BorderColor),
                FontFamily = First(branding?.FontFamily, theme.FontFamily),
                BorderRadius = theme.BorderRadius
            },
            Contact = new ContactBrandingDto
            {
                SupportEmail = branding?.SupportEmail,
                WebsiteUrl = branding?.WebsiteUrl,
                PrivacyUrl = branding?.PrivacyUrl,
                TermsUrl = branding?.TermsUrl
            }
        };
    }

    public static string First(params string?[] values)
    {
        foreach (var value in values)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                return value.Trim();
            }
        }

        return string.Empty;
    }

    private static BrandAsset? FindAsset(IReadOnlyCollection<BrandAsset> assets, AssetType type, Guid? productId)
    {
        return assets.FirstOrDefault(asset =>
            asset.IsActive &&
            asset.Type == type &&
            asset.ProductId == productId);
    }
}
