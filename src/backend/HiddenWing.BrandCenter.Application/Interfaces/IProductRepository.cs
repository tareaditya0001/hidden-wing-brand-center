using HiddenWing.BrandCenter.Domain.Entities;

namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

    Task<Product?> GetBySlugWithBrandingAsync(string slug, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<Product> Items, int Total)> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);

    Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default);

    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);

    Task DeleteAsync(Product product, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default);

    Task<int> CountByBrandIdAsync(Guid brandId, CancellationToken cancellationToken = default);

    Task<int> CountActiveAsync(CancellationToken cancellationToken = default);

    Task<ProductBranding?> GetBrandingByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

    Task<ProductBranding> UpsertBrandingAsync(ProductBranding branding, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Product>> GetRecentlyUpdatedAsync(int take, CancellationToken cancellationToken = default);
}
