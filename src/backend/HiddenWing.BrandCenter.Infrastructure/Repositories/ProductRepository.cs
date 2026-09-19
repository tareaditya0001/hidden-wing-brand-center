using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiddenWing.BrandCenter.Infrastructure.Repositories;

public sealed class ProductRepository : IProductRepository
{
    private readonly BrandDbContext _dbContext;

    public ProductRepository(BrandDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Brand)
            .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
    }

    public Task<Product?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
    {
        return _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Brand)
            .FirstOrDefaultAsync(product => product.Slug == slug, cancellationToken);
    }

    public Task<Product?> GetBySlugWithBrandingAsync(string slug, CancellationToken cancellationToken = default)
    {
        return _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Brand)
            .Include(product => product.Branding)
            .ThenInclude(branding => branding!.Theme)
            .FirstOrDefaultAsync(product => product.Slug == slug, cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .Include(product => product.Brand)
            .OrderBy(product => product.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<(IReadOnlyList<Product> Items, int Total)> GetPagedAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Products.AsNoTracking().Include(product => product.Brand).OrderBy(product => product.Name);
        var total = await query.CountAsync(cancellationToken);
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
        return (items, total);
    }

    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return product;
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _dbContext.Products.Update(product);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        var tracked = await _dbContext.Products.FirstOrDefaultAsync(item => item.Id == product.Id, cancellationToken);
        if (tracked is null)
        {
            return;
        }

        _dbContext.Products.Remove(tracked);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _dbContext.Products.AnyAsync(product => product.Id == id, cancellationToken);
    }

    public Task<bool> SlugExistsAsync(string slug, Guid? excludeId = null, CancellationToken cancellationToken = default)
    {
        return _dbContext.Products.AnyAsync(
            product => product.Slug == slug && (!excludeId.HasValue || product.Id != excludeId.Value),
            cancellationToken);
    }

    public Task<int> CountByBrandIdAsync(Guid brandId, CancellationToken cancellationToken = default)
    {
        return _dbContext.Products.CountAsync(product => product.BrandId == brandId, cancellationToken);
    }

    public Task<int> CountActiveAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.Products.CountAsync(product => product.IsActive, cancellationToken);
    }

    public Task<ProductBranding?> GetBrandingByProductIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return _dbContext.ProductBrandings
            .AsNoTracking()
            .FirstOrDefaultAsync(branding => branding.ProductId == productId, cancellationToken);
    }

    public async Task<ProductBranding> UpsertBrandingAsync(ProductBranding branding, CancellationToken cancellationToken = default)
    {
        var existing = await _dbContext.ProductBrandings
            .FirstOrDefaultAsync(item => item.ProductId == branding.ProductId, cancellationToken);

        if (existing is null)
        {
            _dbContext.ProductBrandings.Add(branding);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return branding;
        }

        existing.ThemeId = branding.ThemeId;
        existing.DisplayName = branding.DisplayName;
        existing.ShortName = branding.ShortName;
        existing.Tagline = branding.Tagline;
        existing.LogoUrl = branding.LogoUrl;
        existing.LogoDarkUrl = branding.LogoDarkUrl;
        existing.LogoLightUrl = branding.LogoLightUrl;
        existing.IconUrl = branding.IconUrl;
        existing.FaviconUrl = branding.FaviconUrl;
        existing.PrimaryColor = branding.PrimaryColor;
        existing.SecondaryColor = branding.SecondaryColor;
        existing.AccentColor = branding.AccentColor;
        existing.BackgroundColor = branding.BackgroundColor;
        existing.SurfaceColor = branding.SurfaceColor;
        existing.TextColor = branding.TextColor;
        existing.MutedTextColor = branding.MutedTextColor;
        existing.BorderColor = branding.BorderColor;
        existing.FontFamily = branding.FontFamily;
        existing.SupportEmail = branding.SupportEmail;
        existing.WebsiteUrl = branding.WebsiteUrl;
        existing.PrivacyUrl = branding.PrivacyUrl;
        existing.TermsUrl = branding.TermsUrl;
        existing.IsEnabled = branding.IsEnabled;
        existing.UpdatedAt = branding.UpdatedAt;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<IReadOnlyList<Product>> GetRecentlyUpdatedAsync(int take, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products
            .AsNoTracking()
            .OrderByDescending(product => product.UpdatedAt)
            .Take(take)
            .ToListAsync(cancellationToken);
    }
}
