using System.Text;
using HiddenWing.BrandCenter.Api.Configuration;
using HiddenWing.BrandCenter.Application;
using HiddenWing.BrandCenter.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using InfrastructureSecurity = HiddenWing.BrandCenter.Infrastructure.Options.SecuritySettings;

namespace HiddenWing.BrandCenter.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBrandCenterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseSettings>(configuration.GetSection(DatabaseSettings.SectionName));
        services.Configure<StorageSettings>(configuration.GetSection(StorageSettings.SectionName));
        services.Configure<SecuritySettings>(configuration.GetSection(SecuritySettings.SectionName));

        services.AddApplication();
        services.AddInfrastructure(configuration);
        services.AddBrandCenterAuthentication(configuration);
        services.AddBrandCenterSwagger();
        services.AddCors(options =>
        {
            var origins = configuration.GetSection("CORS:AllowedOrigins").Get<string[]>() ?? [];
            options.AddPolicy("BrandCenter", policy =>
            {
                if (origins.Length == 0)
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    return;
                }

                policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod();
            });
        });

        return services;
    }

    private static IServiceCollection AddBrandCenterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var secret = configuration[$"{SecuritySettings.SectionName}:JwtSecret"];
        if (string.IsNullOrWhiteSpace(secret) || secret.Length < 32)
        {
            throw new InvalidOperationException("Security:JwtSecret must be configured with at least 32 characters.");
        }

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.AddSingleton<IConfigureOptions<JwtBearerOptions>, JwtBearerOptionsSetup>();
        services.AddAuthorization();
        return services;
    }

    private static IServiceCollection AddBrandCenterSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Hidden Wing Brand Center API",
                Version = "v1",
                Description = "Central branding and white-label configuration for Hidden Wing applications."
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}

internal sealed class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IOptionsMonitor<InfrastructureSecurity> _security;

    public JwtBearerOptionsSetup(IOptionsMonitor<InfrastructureSecurity> security)
    {
        _security = security;
    }

    public void Configure(JwtBearerOptions options)
    {
        Configure(JwtBearerDefaults.AuthenticationScheme, options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        var security = _security.CurrentValue;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = security.JwtIssuer,
            ValidAudience = security.JwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(security.JwtSecret))
            {
                KeyId = "hidden-wing-brand-center"
            }
        };
    }
}
