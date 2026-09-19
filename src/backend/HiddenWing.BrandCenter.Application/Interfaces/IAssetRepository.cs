using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Domain.Enums;

namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IAssetRepository
{
    Task<BrandAsset?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BrandAsset>> GetAllAsync(Guid? brandId, Guid? productId, CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<BrandAsset> Items, int Total)> GetPagedAsync(int page, int pageSize, Guid? brandId, Guid? productId, CancellationToken cancellationToken = default);

    Task<BrandAsset?> GetByBrandAndTypeAsync(Guid brandId, AssetType type, Guid? productId, CancellationToken cancellationToken = default);

    Task<BrandAsset> CreateAsync(BrandAsset asset, CancellationToken cancellationToken = default);

    Task UpdateAsync(BrandAsset asset, CancellationToken cancellationToken = default);

    Task DeleteAsync(BrandAsset asset, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<BrandAsset>> GetRecentlyUpdatedAsync(int take, CancellationToken cancellationToken = default);
}
