using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Domain.Enums;
using HiddenWing.BrandCenter.Domain.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiddenWing.BrandCenter.Infrastructure.Persistence.Configurations;

public sealed class BrandAssetConfiguration : IEntityTypeConfiguration<BrandAsset>
{
    public void Configure(EntityTypeBuilder<BrandAsset> builder)
    {
        builder.ToTable("brand_assets");
        builder.HasKey(asset => asset.Id);
        builder.Property(asset => asset.Name).HasMaxLength(160).IsRequired();
        builder.Property(asset => asset.Url).HasMaxLength(1000).IsRequired();
        builder.Property(asset => asset.MimeType).HasMaxLength(120);
        builder.Property(asset => asset.Type).HasConversion<string>().HasMaxLength(32);

        builder.HasOne(asset => asset.Brand)
            .WithMany(brand => brand.Assets)
            .HasForeignKey(asset => asset.BrandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(asset => asset.Product)
            .WithMany(product => product.Assets)
            .HasForeignKey(asset => asset.ProductId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasData(
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000041"), "Hidden Wing Logo", AssetType.Logo, "/assets/hidden-wing/logo.svg", "image/svg+xml"),
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000042"), "Hidden Wing Logo Dark", AssetType.LogoDark, "/assets/hidden-wing/logo-dark.svg", "image/svg+xml"),
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000043"), "Hidden Wing Logo Light", AssetType.LogoLight, "/assets/hidden-wing/logo-light.svg", "image/svg+xml"),
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000044"), "Hidden Wing Icon", AssetType.Icon, "/assets/hidden-wing/icon.svg", "image/svg+xml"),
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000045"), "Hidden Wing Favicon", AssetType.Favicon, "/assets/hidden-wing/favicon.svg", "image/svg+xml"),
            Create(Guid.Parse("8a0f1c2e-3b4d-4e5f-9a01-000000000046"), "Hidden Wing OG Image", AssetType.OgImage, "/assets/hidden-wing/og-image.png", "image/png"));
    }

    private static BrandAsset Create(Guid id, string name, AssetType type, string url, string mimeType)
    {
        return new BrandAsset
        {
            Id = id,
            BrandId = SeedIdentifiers.HiddenWingBrandId,
            Name = name,
            Type = type,
            Url = url,
            MimeType = mimeType,
            FileSize = 0,
            IsActive = true,
            CreatedAt = SeedDates.CreatedAt,
            UpdatedAt = SeedDates.CreatedAt
        };
    }
}
