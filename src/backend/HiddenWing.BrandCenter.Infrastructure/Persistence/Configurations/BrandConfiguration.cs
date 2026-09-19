using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Domain.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiddenWing.BrandCenter.Infrastructure.Persistence.Configurations;

public sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("brands");
        builder.HasKey(brand => brand.Id);
        builder.Property(brand => brand.Name).HasMaxLength(120).IsRequired();
        builder.Property(brand => brand.Slug).HasMaxLength(80).IsRequired();
        builder.Property(brand => brand.CompanyName).HasMaxLength(160).IsRequired();
        builder.Property(brand => brand.Description).HasMaxLength(500);
        builder.HasIndex(brand => brand.Slug).IsUnique();

        builder.HasData(new Brand
        {
            Id = SeedIdentifiers.HiddenWingBrandId,
            Name = "Hidden Wing",
            Slug = "hidden-wing",
            CompanyName = "Hidden Wing",
            Description = "Technology, products, solutions and digital ventures built under Hidden Wing.",
            IsActive = true,
            CreatedAt = SeedDates.CreatedAt,
            UpdatedAt = SeedDates.CreatedAt
        });
    }
}
