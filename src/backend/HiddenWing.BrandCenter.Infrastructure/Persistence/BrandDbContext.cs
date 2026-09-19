using HiddenWing.BrandCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HiddenWing.BrandCenter.Infrastructure.Persistence;

public sealed class BrandDbContext : DbContext
{
    public BrandDbContext(DbContextOptions<BrandDbContext> options)
        : base(options)
    {
    }

    public DbSet<Brand> Brands => Set<Brand>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductBranding> ProductBrandings => Set<ProductBranding>();

    public DbSet<Theme> Themes => Set<Theme>();

    public DbSet<BrandAsset> BrandAssets => Set<BrandAsset>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BrandDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
