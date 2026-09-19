using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Domain.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiddenWing.BrandCenter.Infrastructure.Persistence.Configurations;

public sealed class ProductBrandingConfiguration : IEntityTypeConfiguration<ProductBranding>
{
    public void Configure(EntityTypeBuilder<ProductBranding> builder)
    {
        builder.ToTable("product_brandings");
        builder.HasKey(branding => branding.Id);
        builder.HasIndex(branding => branding.ProductId).IsUnique();
        builder.Property(branding => branding.DisplayName).HasMaxLength(160);
        builder.Property(branding => branding.ShortName).HasMaxLength(80);
        builder.Property(branding => branding.Tagline).HasMaxLength(240);
        builder.Property(branding => branding.LogoUrl).HasMaxLength(1000);
        builder.Property(branding => branding.LogoDarkUrl).HasMaxLength(1000);
        builder.Property(branding => branding.LogoLightUrl).HasMaxLength(1000);
        builder.Property(branding => branding.IconUrl).HasMaxLength(1000);
        builder.Property(branding => branding.FaviconUrl).HasMaxLength(1000);
        builder.Property(branding => branding.PrimaryColor).HasMaxLength(32);
        builder.Property(branding => branding.SecondaryColor).HasMaxLength(32);
        builder.Property(branding => branding.AccentColor).HasMaxLength(32);
        builder.Property(branding => branding.BackgroundColor).HasMaxLength(32);
        builder.Property(branding => branding.SurfaceColor).HasMaxLength(32);
        builder.Property(branding => branding.TextColor).HasMaxLength(32);
        builder.Property(branding => branding.MutedTextColor).HasMaxLength(32);
        builder.Property(branding => branding.BorderColor).HasMaxLength(32);
        builder.Property(branding => branding.FontFamily).HasMaxLength(160);
        builder.Property(branding => branding.SupportEmail).HasMaxLength(200);
        builder.Property(branding => branding.WebsiteUrl).HasMaxLength(500);
        builder.Property(branding => branding.PrivacyUrl).HasMaxLength(500);
        builder.Property(branding => branding.TermsUrl).HasMaxLength(500);

        builder.HasOne(branding => branding.Theme)
            .WithMany(theme => theme.ProductBrandings)
            .HasForeignKey(branding => branding.ThemeId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasData(
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000031"), SeedIdentifiers.HqProductId, "Hidden Wing HQ", "HQ", "Company operations, in one place."),
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000032"), SeedIdentifiers.StoreProductId, "Hidden Wing Store", "Store", "Products and commerce under Hidden Wing."),
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000033"), SeedIdentifiers.ProjectsProductId, "Hidden Wing Projects", "Projects", "Plan and deliver Hidden Wing work."),
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000034"), SeedIdentifiers.DataProductId, "Hidden Wing Data", "Data", "Insight from Hidden Wing systems."),
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000035"), SeedIdentifiers.AdminProductId, "Hidden Wing Admin", "Admin", "Operate Hidden Wing platforms."));
    }

    private static ProductBranding Create(Guid id, Guid productId, string displayName, string shortName, string tagline)
    {
        return new ProductBranding
        {
            Id = id,
            ProductId = productId,
            ThemeId = SeedIdentifiers.DefaultThemeId,
            DisplayName = displayName,
            ShortName = shortName,
            Tagline = tagline,
            IsEnabled = true,
            CreatedAt = SeedDates.CreatedAt,
            UpdatedAt = SeedDates.CreatedAt
        };
    }
}
