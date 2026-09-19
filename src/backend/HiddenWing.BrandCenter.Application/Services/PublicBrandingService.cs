using HiddenWing.BrandCenter.Application.Branding;
using HiddenWing.BrandCenter.Application.DTOs.Branding;
using HiddenWing.BrandCenter.Application.Exceptions;
using HiddenWing.BrandCenter.Application.Interfaces;

namespace HiddenWing.BrandCenter.Application.Services;

/// <summary>
/// Resolves public branding for a product slug.
/// The service boundary is intentionally small so caching can wrap this class later.
/// </summary>
public sealed class PublicBrandingService : IPublicBrandingService
{
    private readonly IProductRepository _productRepository;
    private readonly IThemeRepository _themeRepository;
    private readonly IAssetRepository _assetRepository;

    public PublicBrandingService(
        IProductRepository productRepository,
        IThemeRepository themeRepository,
        IAssetRepository assetRepository)
    {
        _productRepository = productRepository;
        _themeRepository = themeRepository;
        _assetRepository = assetRepository;
    }

    public async Task<PublicBrandingDto> GetByProductSlugAsync(string productSlug, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetBySlugWithBrandingAsync(productSlug, cancellationToken);
        if (product is null || !product.IsActive || product.Branding is { IsEnabled: false })
        {
            throw new NotFoundException("Product branding not found.", "PUBLIC_BRANDING_NOT_FOUND");
        }

        var defaultTheme = await _themeRepository.GetDefaultAsync(cancellationToken);
        if (defaultTheme is null)
        {
            throw new NotFoundException("A default theme has not been configured.", "DEFAULT_THEME_MISSING");
        }

        var assignedTheme = product.Branding?.Theme;
        var assets = await _assetRepository.GetAllAsync(product.BrandId, productId: null, cancellationToken);
        var productAssets = await _assetRepository.GetAllAsync(product.BrandId, product.Id, cancellationToken);
        var combinedAssets = assets.Concat(productAssets).ToList();

        return BrandingResolver.Resolve(
            product.Brand,
            product,
            product.Branding,
            assignedTheme,
            defaultTheme,
            combinedAssets);
    }
}
