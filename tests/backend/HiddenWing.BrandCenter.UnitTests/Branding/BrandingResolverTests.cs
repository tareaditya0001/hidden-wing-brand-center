using FluentAssertions;
using HiddenWing.BrandCenter.Application.Branding;
using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Domain.Enums;

namespace HiddenWing.BrandCenter.UnitTests.Branding;

public sealed class BrandingResolverTests
{
    [Fact]
    public void Resolve_UsesProductOverride_WhenFieldIsSet()
    {
        var (brand, product, branding, defaultTheme, assets) = CreateGraph();
        branding.PrimaryColor = "#6B3FA0";
        branding.DisplayName = "Hidden Wing Store";

        var result = BrandingResolver.Resolve(brand, product, branding, assignedTheme: null, defaultTheme, assets);

        result.Product.Name.Should().Be("Hidden Wing Store");
        result.Theme.PrimaryColor.Should().Be("#6B3FA0");
        result.Theme.FontFamily.Should().Be("Inter, system-ui, sans-serif");
    }

    [Fact]
    public void Resolve_FallsBackToDefaultTheme_WhenProductFieldIsMissing()
    {
        var (brand, product, branding, defaultTheme, assets) = CreateGraph();
        branding.FontFamily = null;
        branding.PrimaryColor = null;

        var result = BrandingResolver.Resolve(brand, product, branding, assignedTheme: null, defaultTheme, assets);

        result.Theme.PrimaryColor.Should().Be("#1C3353");
        result.Theme.FontFamily.Should().Be("Inter, system-ui, sans-serif");
        result.Company.Name.Should().Be("Hidden Wing");
    }

    [Fact]
    public void Resolve_PrefersAssignedTheme_OverDefaultTheme()
    {
        var (brand, product, branding, defaultTheme, assets) = CreateGraph();
        var assignedTheme = CloneTheme(defaultTheme);
        assignedTheme.PrimaryColor = "#111827";

        var result = BrandingResolver.Resolve(brand, product, branding, assignedTheme, defaultTheme, assets);

        result.Theme.PrimaryColor.Should().Be("#111827");
    }

    [Fact]
    public void Resolve_UsesProductAsset_WhenOverrideUrlIsMissing()
    {
        var (brand, product, branding, defaultTheme, assets) = CreateGraph();
        branding.LogoUrl = null;

        var result = BrandingResolver.Resolve(brand, product, branding, assignedTheme: null, defaultTheme, assets);

        result.Product.Logo.Should().Be("/assets/store/logo.svg");
        result.Company.Logo.Should().Be("/assets/hidden-wing/logo.svg");
    }

    private static (Brand Brand, Product Product, ProductBranding Branding, Theme DefaultTheme, List<BrandAsset> Assets) CreateGraph()
    {
        var brand = new Brand
        {
            Id = Guid.NewGuid(),
            Name = "Hidden Wing",
            Slug = "hidden-wing",
            CompanyName = "Hidden Wing"
        };

        var product = new Product
        {
            Id = Guid.NewGuid(),
            BrandId = brand.Id,
            Name = "Hidden Wing Store",
            Slug = "store"
        };

        var branding = new ProductBranding
        {
            ProductId = product.Id,
            IsEnabled = true
        };

        var defaultTheme = new Theme
        {
            Name = "Hidden Wing Default",
            Slug = "hidden-wing-default",
            PrimaryColor = "#1C3353",
            SecondaryColor = "#3E536B",
            AccentColor = "#C6A15B",
            BackgroundColor = "#F4EFE6",
            SurfaceColor = "#FFFFFF",
            TextColor = "#1A1F29",
            MutedTextColor = "#5C6B7A",
            BorderColor = "#D9D2C5",
            FontFamily = "Inter, system-ui, sans-serif",
            BorderRadius = "8px",
            IsDefault = true
        };

        var assets = new List<BrandAsset>
        {
            new()
            {
                BrandId = brand.Id,
                Type = AssetType.Logo,
                Url = "/assets/hidden-wing/logo.svg",
                IsActive = true
            },
            new()
            {
                BrandId = brand.Id,
                ProductId = product.Id,
                Type = AssetType.Logo,
                Url = "/assets/store/logo.svg",
                IsActive = true
            }
        };

        return (brand, product, branding, defaultTheme, assets);
    }

    private static Theme CloneTheme(Theme theme)
    {
        return new Theme
        {
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
            IsDefault = false
        };
    }
}
