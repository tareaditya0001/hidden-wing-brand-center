using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Domain.Enums;
using HiddenWing.BrandCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiddenWing.BrandCenter.Infrastructure.Repositories;

public sealed class AssetRepository : IAssetRepository
{
    private readonly BrandDbContext _dbContext;

    public AssetRepository(BrandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<BrandAsset?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.BrandAssets.AsNoTracking().FirstOrDefaultAsync(asset => asset.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<BrandAsset>> GetAllAsync(Guid? brandId, Guid? productId, CancellationToken cancellationToken = default)
    {
        return await ApplyFilters(_dbContext.BrandAssets.AsNoTracking(), brandId, productId)
            .OrderBy(asset => asset.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<BrandAsset> Items, int Total)> GetPagedAsync(
        int page,
        int pageSize,
        Guid? brandId,
        Guid? productId,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyFilters(_dbContext.BrandAssets.AsNoTracking(), brandId, productId).OrderBy(asset => asset.Name);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public Task<BrandAsset?> GetByBrandAndTypeAsync(Guid brandId, AssetType type, Guid? productId, CancellationToken cancellationToken = default)
    {
        return _dbContext.BrandAssets
            .AsNoTracking()
            .FirstOrDefaultAsync(
                asset => asset.BrandId == brandId && asset.Type == type && asset.ProductId == productId,
                cancellationToken);
    }

    public async Task<BrandAsset> CreateAsync(BrandAsset asset, CancellationToken cancellationToken = default)
    {
        _dbContext.BrandAssets.Add(asset);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return asset;
    }

    public async Task UpdateAsync(BrandAsset asset, CancellationToken cancellationToken = default)
    {
        _dbContext.BrandAssets.Update(asset);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(BrandAsset asset, CancellationToken cancellationToken = default)
    {
        var tracked = await _dbContext.BrandAssets.FirstOrDefaultAsync(item => item.Id == asset.Id, cancellationToken);
        if (tracked is null)
        {
            return;
        }

        _dbContext.BrandAssets.Remove(tracked);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.BrandAssets.CountAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<BrandAsset>> GetRecentlyUpdatedAsync(int take, CancellationToken cancellationToken = default)
    {
        return await _dbContext.BrandAssets
            .AsNoTracking()
            .OrderByDescending(asset => asset.UpdatedAt)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    private static IQueryable<BrandAsset> ApplyFilters(IQueryable<BrandAsset> query, Guid? brandId, Guid? productId)
    {
        if (brandId.HasValue)
        {
            query = query.Where(asset => asset.BrandId == brandId.Value);
        }

        if (productId.HasValue)
        {
            query = query.Where(asset => asset.ProductId == productId.Value);
        }

        return query;
    }
}
