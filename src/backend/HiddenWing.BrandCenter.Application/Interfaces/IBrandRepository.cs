using HiddenWing.BrandCenter.Domain.Entities;

namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IBrandRepository
{
    Task<Brand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Brand?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Brand>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<Brand> Items, int Total)> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);

    Task<Brand> CreateAsync(Brand brand, CancellationToken cancellationToken = default);

    Task UpdateAsync(Brand brand, CancellationToken cancellationToken = default);

    Task DeleteAsync(Brand brand, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default);

    Task<int> CountActiveAsync(CancellationToken cancellationToken = default);
}
