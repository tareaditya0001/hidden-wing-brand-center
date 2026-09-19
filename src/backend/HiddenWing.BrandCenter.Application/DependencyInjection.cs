using FluentValidation;
using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HiddenWing.BrandCenter.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAssetService, AssetService>();
        services.AddScoped<IThemeService, ThemeService>();
        services.AddScoped<IPublicBrandingService, PublicBrandingService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
}
