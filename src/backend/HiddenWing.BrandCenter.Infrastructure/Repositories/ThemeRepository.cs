using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiddenWing.BrandCenter.Infrastructure.Repositories;

public sealed class ThemeRepository : IThemeRepository
{
    private readonly BrandDbContext _dbContext;

    public ThemeRepository(BrandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Theme?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Themes.AsNoTracking().FirstOrDefaultAsync(theme => theme.Id == id, cancellationToken);
    }

    public Task<Theme?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return _dbContext.Themes.AsNoTracking().FirstOrDefaultAsync(theme => theme.Slug == slug, cancellationToken);
    }

    public Task<Theme?> GetDefaultAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Themes.AsNoTracking().FirstOrDefaultAsync(theme => theme.IsDefault, cancellationToken);
    }

    public async Task<IReadOnlyList<Theme>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Themes.AsNoTracking().OrderBy(theme => theme.Name).ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Theme> Items, int Total)> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Themes.AsNoTracking().OrderByDescending(theme => theme.IsDefault).ThenBy(theme => theme.Name);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public async Task<Theme> CreateAsync(Theme theme, CancellationToken cancellationToken = default)
    {
        _dbContext.Themes.Add(theme);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return theme;
    }

    public async Task UpdateAsync(Theme theme, CancellationToken cancellationToken = default)
    {
        _dbContext.Themes.Update(theme);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Theme theme, CancellationToken cancellationToken = default)
    {
        var tracked = await _dbContext.Themes.FirstOrDefaultAsync(item => item.Id == theme.Id, cancellationToken);
        if (tracked is null)
        {
            return;
        }

        _dbContext.Themes.Remove(tracked);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        return _dbContext.Themes.AnyAsync(
            theme => theme.Slug == slug && (!excludeId.HasValue || theme.Id != excludeId.Value),
            cancellationToken);
    }

    public async Task ClearDefaultAsync(Guid? exceptThemeId, CancellationToken cancellationToken = default)
    {
        var defaults = await _dbContext.Themes
            .Where(theme => theme.IsDefault && (!exceptThemeId.HasValue || theme.Id != exceptThemeId.Value))
            .ToListAsync(cancellationToken);

        foreach (var theme in defaults)
        {
            theme.IsDefault = false;
            theme.UpdatedAt = DateTime.UtcNow;
        }

        if (defaults.Count > 0)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Themes.CountAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Theme>> GetRecentlyUpdatedAsync(int take, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Themes
            .AsNoTracking()
            .OrderByDescending(theme => theme.UpdatedAt)
            .Take(take)
            .ToListAsync(cancellationToken);
    }
}
