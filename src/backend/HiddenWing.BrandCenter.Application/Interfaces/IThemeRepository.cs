using HiddenWing.BrandCenter.Domain.Entities;

namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IThemeRepository
{
    Task<Theme?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Theme?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default);

    Task<Theme?> GetDefaultAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Theme>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<(IReadOnlyList<Theme> Items, int Total)> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default);

    Task<Theme> CreateAsync(Theme theme, CancellationToken cancellationToken = default);

    Task UpdateAsync(Theme theme, CancellationToken cancellationToken = default);

    Task DeleteAsync(Theme theme, CancellationToken cancellationToken = default);

    Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default);

    Task ClearDefaultAsync(Guid? exceptThemeId, CancellationToken cancellationToken = default);

    Task<int> CountAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Theme>> GetRecentlyUpdatedAsync(int take, CancellationToken cancellationToken = default);
}
