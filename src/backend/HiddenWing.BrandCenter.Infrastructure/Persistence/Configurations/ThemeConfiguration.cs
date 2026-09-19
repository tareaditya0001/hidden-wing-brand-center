using HiddenWing.BrandCenter.Domain.Entities;
using HiddenWing.BrandCenter.Domain.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HiddenWing.BrandCenter.Infrastructure.Persistence.Configurations;

public sealed class ThemeConfiguration : IEntityTypeConfiguration<Theme>
{
    public void Configure(EntityTypeBuilder<Theme> builder)
    {
        builder.ToTable("themes");
        builder.HasKey(theme => theme.Id);
        builder.Property(theme => theme.Name).HasMaxLength(120).IsRequired();
        builder.Property(theme => theme.Slug).HasMaxLength(80).IsRequired();
        builder.Property(theme => theme.PrimaryColor).HasMaxLength(32).IsRequired();
        builder.Property(theme => theme.SecondaryColor).HasMaxLength(32).IsRequired();
        builder.Property(theme => theme.AccentColor).HasMaxLength(32).IsRequired();
        builder.Property(theme => theme.BackgroundColor).HasMaxLength(32).IsRequired();
        builder.Property(theme => theme.SurfaceColor).HasMaxLength(32).IsRequired();
        builder.Property(theme => theme.TextColor).HasMaxLength(32).IsRequired();
        builder.Property(theme => theme.MutedTextColor).HasMaxLength(32).IsRequired();
        builder.Property(theme => theme.BorderColor).HasMaxLength(32).IsRequired();
        builder.Property(theme => theme.FontFamily).HasMaxLength(160).IsRequired();
        builder.Property(theme => theme.BorderRadius).HasMaxLength(32).IsRequired();
        builder.HasIndex(theme => theme.Slug).IsUnique();

        builder.HasData(new Theme
        {
            Id = SeedIdentifiers.DefaultThemeId,
            Name = "Hidden Wing Default",
            Slug = "hidden-wing-default",
            PrimaryColor = "#1C3353",
            SecondaryColor = "#3E536B",
            AccentColor = "#C6A15B",
            BackgroundColor = "#F4EFE6",
            SurfaceColor = "#FFFFFF",
            TextColor = "#1A1F29",
            MutedTextColor = "#5C6B7A",
            BorderColor = "#D9D2C5",
            FontFamily = "Inter, system-ui, sans-serif",
            BorderRadius = "8px",
            IsDefault = true,
            CreatedAt = SeedDates.CreatedAt,
            UpdatedAt = SeedDates.CreatedAt
        });
    }
}
