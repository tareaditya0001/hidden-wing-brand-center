using HiddenWing.BrandCenter.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HiddenWing.BrandCenter.IntegrationTests;

public sealed class BrandCenterApiFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = $"brand-center-{Guid.NewGuid():N}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.ConfigureAppConfiguration((_, configuration) =>
        {
            configuration.AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = "Host=localhost;Database=unused;Username=unused;Password=unused",
                ["Database:ApplyMigrations"] = "false",
                ["Security:JwtSecret"] = "INTEGRATION_TEST_SECRET_HIDDEN_WING_32",
                ["Security:AdminUsername"] = "admin",
                ["Security:AdminPassword"] = "admin"
            });
        });

        builder.ConfigureTestServices(services =>
        {
            RemoveDbContext(services);
            services.AddDbContext<BrandDbContext>(options => options.UseInMemoryDatabase(_databaseName));
        });
    }

    public async Task<HttpClient> CreateReadyClientAsync()
    {
        var client = CreateClient();
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BrandDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
        return client;
    }

    private static void RemoveDbContext(IServiceCollection services)
    {
        var stale = services
            .Where(descriptor =>
                descriptor.ServiceType == typeof(BrandDbContext) ||
                descriptor.ServiceType == typeof(DbContextOptions<BrandDbContext>) ||
                descriptor.ServiceType == typeof(DbContextOptions) ||
                (descriptor.ServiceType.IsGenericType &&
                 descriptor.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>)))
            .ToList();

        foreach (var descriptor in stale)
        {
            services.Remove(descriptor);
        }
    }
}
