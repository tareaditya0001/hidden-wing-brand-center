using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Infrastructure.Options;
using HiddenWing.BrandCenter.Infrastructure.Persistence;
using HiddenWing.BrandCenter.Infrastructure.Repositories;
using HiddenWing.BrandCenter.Infrastructure.Security;
using HiddenWing.BrandCenter.Infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HiddenWing.BrandCenter.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StorageSettings>(configuration.GetSection(StorageSettings.SectionName));
        services.Configure<SecuritySettings>(configuration.GetSection(SecuritySettings.SectionName));

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is not configured.");

        services.AddDbContext<BrandDbContext>(options => options.UseNpgsql(connectionString));
        services.AddHealthChecks().AddDbContextCheck<BrandDbContext>("database");

        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IAssetRepository, AssetRepository>();
        services.AddScoped<IThemeRepository, ThemeRepository>();
        services.AddScoped<IAssetStorageService, AssetStorageService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
