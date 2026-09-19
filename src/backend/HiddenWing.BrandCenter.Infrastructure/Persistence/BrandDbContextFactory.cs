using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HiddenWing.BrandCenter.Infrastructure.Persistence;

/// <summary>
/// Design-time factory used by EF Core CLI migrations.
/// </summary>
public sealed class BrandDbContextFactory : IDesignTimeDbContextFactory<BrandDbContext>
{
    public BrandDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "HiddenWing.BrandCenter.Api"))
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? "Host=localhost;Port=5432;Database=hidden_wing_brand_center;Username=brandcenter;Password=brandcenter_dev";

        var options = new DbContextOptionsBuilder<BrandDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new BrandDbContext(options);
    }
}
