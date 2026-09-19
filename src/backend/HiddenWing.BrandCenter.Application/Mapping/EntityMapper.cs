using HiddenWing.BrandCenter.Application.DTOs.Asset;
using HiddenWing.BrandCenter.Application.DTOs.Brand;
using HiddenWing.BrandCenter.Application.DTOs.Product;
using HiddenWing.BrandCenter.Application.DTOs.Theme;
using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Domain.Enums;

namespace HiddenWing.BrandCenter.Application.Mapping;

public static class EntityMapper
{
    public static BrandDto ToDto(Brand brand)
    {
        return new BrandDto
        {
            Id = brand.Id,
            Name = brand.Name,
            Slug = brand.Slug,
            CompanyName = brand.CompanyName,
            Description = brand.Description,
            IsActive = brand.IsActive,
            CreatedAt = brand.CreatedAt,
            UpdatedAt = brand.UpdatedAt
        };
    }

    public static ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            Id = product.Id,
            BrandId = product.BrandId,
            BrandName = product.Brand?.Name ?? string.Empty,
            Name = product.Name,
            Slug = product.Slug,
            Description = product.Description,
            ApplicationUrl = product.ApplicationUrl,
            IsActive = product.IsActive,
            Status = product.Status,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
    }

    public static ProductBrandingDto ToDto(ProductBranding branding)
    {
        return new ProductBrandingDto
        {
            Id = branding.Id,
            ProductId = branding.ProductId,
            ThemeId = branding.ThemeId,
            DisplayName = branding.DisplayName,
            ShortName = branding.ShortName,
            Tagline = branding.Tagline,
            LogoUrl = branding.LogoUrl,
            LogoDarkUrl = branding.LogoDarkUrl,
            LogoLightUrl = branding.LogoLightUrl,
            IconUrl = branding.IconUrl,
            FaviconUrl = branding.FaviconUrl,
            PrimaryColor = branding.PrimaryColor,
            SecondaryColor = branding.SecondaryColor,
            AccentColor = branding.AccentColor,
            BackgroundColor = branding.BackgroundColor,
            SurfaceColor = branding.SurfaceColor,
            TextColor = branding.TextColor,
            MutedTextColor = branding.MutedTextColor,
            BorderColor = branding.BorderColor,
            FontFamily = branding.FontFamily,
            SupportEmail = branding.SupportEmail,
            WebsiteUrl = branding.WebsiteUrl,
            PrivacyUrl = branding.PrivacyUrl,
            TermsUrl = branding.TermsUrl,
            IsEnabled = branding.IsEnabled,
            CreatedAt = branding.CreatedAt,
            UpdatedAt = branding.UpdatedAt
        };
    }

    public static ThemeDto ToDto(Theme theme)
    {
        return new ThemeDto
        {
            Id = theme.Id,
            Name = theme.Name,
            Slug = theme.Slug,
            PrimaryColor = theme.PrimaryColor,
            SecondaryColor = theme.SecondaryColor,
            AccentColor = theme.AccentColor,
            BackgroundColor = theme.BackgroundColor,
            SurfaceColor = theme.SurfaceColor,
            TextColor = theme.TextColor,
            MutedTextColor = theme.MutedTextColor,
            BorderColor = theme.BorderColor,
            FontFamily = theme.FontFamily,
            BorderRadius = theme.BorderRadius,
            IsDefault = theme.IsDefault,
            CreatedAt = theme.CreatedAt,
            UpdatedAt = theme.UpdatedAt
        };
    }

    public static AssetDto ToDto(BrandAsset asset)
    {
        return new AssetDto
        {
            Id = asset.Id,
            BrandId = asset.BrandId,
            ProductId = asset.ProductId,
            Name = asset.Name,
            Type = asset.Type,
            Url = asset.Url,
            MimeType = asset.MimeType,
            FileSize = asset.FileSize,
            IsActive = asset.IsActive,
            CreatedAt = asset.CreatedAt,
            UpdatedAt = asset.UpdatedAt
        };
    }

    public static ProductStatus ToStatus(bool isActive)
    {
        return isActive ? ProductStatus.Active : ProductStatus.Inactive;
    }
}
