using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Domain.Enums;
using HiddenWing.BrandCenter.Domain.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiddenWing.BrandCenter.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(product => product.Id);
        builder.Property(product => product.Name).HasMaxLength(120).IsRequired();
        builder.Property(product => product.Slug).HasMaxLength(80).IsRequired();
        builder.Property(product => product.Description).HasMaxLength(500);
        builder.Property(product => product.ApplicationUrl).HasMaxLength(500);
        builder.Property(product => product.Status).HasConversion<string>().HasMaxLength(32);
        builder.HasIndex(product => product.Slug).IsUnique();

        builder.HasOne(product => product.Brand)
            .WithMany(brand => brand.Products)
            .HasForeignKey(product => product.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(product => product.Branding)
            .WithOne(branding => branding.Product)
            .HasForeignKey<ProductBranding>(branding => branding.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            Create(SeedIdentifiers.HqProductId, "Hidden Wing HQ", "hq", "Internal operations and company headquarters workspace."),
            Create(SeedIdentifiers.StoreProductId, "Hidden Wing Store", "store", "Commerce and catalog experience for Hidden Wing products."),
            Create(SeedIdentifiers.ProjectsProductId, "Hidden Wing Projects", "projects", "Project delivery and collaboration workspace."),
            Create(SeedIdentifiers.DataProductId, "Hidden Wing Data", "data", "Data platform and reporting workspace."),
            Create(SeedIdentifiers.AdminProductId, "Hidden Wing Admin", "admin", "Administrative console for Hidden Wing operators."));
    }

    private static Product Create(Guid id, string name, string slug, string description)
    {
        return new Product
        {
            Id = id,
            BrandId = SeedIdentifiers.HiddenWingBrandId,
            Name = name,
            Slug = slug,
            Description = description,
            IsActive = true,
            Status = ProductStatus.Active,
            CreatedAt = SeedDates.CreatedAt,
            UpdatedAt = SeedDates.CreatedAt
        };
    }
}
