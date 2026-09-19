using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiddenWing.BrandCenter.Infrastructure.Repositories;

public sealed class BrandRepository : IBrandRepository
{
    private readonly BrandDbContext _dbContext;

    public BrandRepository(BrandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Brand?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Brands.AsNoTracking().FirstOrDefaultAsync(brand => brand.Id == id, cancellationToken);
    }

    public Task<Brand?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return _dbContext.Brands.AsNoTracking().FirstOrDefaultAsync(brand => brand.Slug == slug, cancellationToken);
    }

    public async Task<IReadOnlyList<Brand>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Brands.AsNoTracking().OrderBy(brand => brand.Name).ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Brand> Items, int Total)> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Brands.AsNoTracking().OrderBy(brand => brand.Name);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public async Task<Brand> CreateAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        _dbContext.Brands.Add(brand);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return brand;
    }

    public async Task UpdateAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        _dbContext.Brands.Update(brand);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        _dbContext.Brands.Remove(brand);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Brands.AnyAsync(brand => brand.Id == id, cancellationToken);
    }

    public Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        return _dbContext.Brands.AnyAsync(
            brand => brand.Slug == slug && (!excludeId.HasValue || brand.Id != excludeId.Value),
            cancellationToken);
    }

    public Task<int> CountActiveAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Brands.CountAsync(brand => brand.IsActive, cancellationToken);
    }
}
