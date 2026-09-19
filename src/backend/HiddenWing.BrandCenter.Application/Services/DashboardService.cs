using HiddenWing.BrandCenter.Application.DTOs.Dashboard;
using HiddenWing.BrandCenter.Application.Interfaces;

namespace HiddenWing.BrandCenter.Application.Services;

public sealed class DashboardService : IDashboardService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IProductRepository _productRepository;
    private readonly IThemeRepository _themeRepository;
    private readonly IAssetRepository _assetRepository;

    public DashboardService(
        IBrandRepository brandRepository,
        IProductRepository productRepository,
        IThemeRepository themeRepository,
        IAssetRepository assetRepository)
    {
        _brandRepository = brandRepository;
        _productRepository = productRepository;
        _themeRepository = themeRepository;
        _assetRepository = assetRepository;
    }

    public async Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default)
    {
        var activeBrands = await _brandRepository.CountActiveAsync(cancellationToken);
        var activeProducts = await _productRepository.CountActiveAsync(cancellationToken);
        var themes = await _themeRepository.CountAsync(cancellationToken);
        var assets = await _assetRepository.CountAsync(cancellationToken);

        var recentProducts = await _productRepository.GetRecentlyUpdatedAsync(5, cancellationToken);
        var recentThemes = await _themeRepository.GetRecentlyUpdatedAsync(5, cancellationToken);
        var recentAssets = await _assetRepository.GetRecentlyUpdatedAsync(5, cancellationToken);

        var recentChanges = recentProducts
            .Select(product => new RecentChangeDto
            {
                EntityType = "Product",
                Name = product.Name,
                UpdatedAt = product.UpdatedAt
            })
            .Concat(recentThemes.Select(theme => new RecentChangeDto
            {
                EntityType = "Theme",
                Name = theme.Name,
                UpdatedAt = theme.UpdatedAt
            }))
            .Concat(recentAssets.Select(asset => new RecentChangeDto
            {
                EntityType = "Asset",
                Name = asset.Name,
                UpdatedAt = asset.UpdatedAt
            }))
            .OrderByDescending(change => change.UpdatedAt)
            .Take(8)
            .ToList();

        return new DashboardSummaryDto
        {
            ActiveBrands = activeBrands,
            ActiveProducts = activeProducts,
            Themes = themes,
            Assets = assets,
            RecentChanges = recentChanges
        };
    }
}
