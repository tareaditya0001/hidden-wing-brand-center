using HiddenWing.BrandCenter.Api.Middleware;
using HiddenWing.BrandCenter.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HiddenWing.BrandCenter.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseBrandCenterPipeline(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hidden Wing Brand Center v1");
            });
        }

        app.UseCors("BrandCenter");
        app.UseStaticFiles();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    public static async Task ApplyMigrationsAsync(this WebApplication app)
    {
        var applyMigrations = app.Configuration.GetValue("Database:ApplyMigrations", false);
        if (!applyMigrations)
        {
            return;
        }

        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<BrandDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
