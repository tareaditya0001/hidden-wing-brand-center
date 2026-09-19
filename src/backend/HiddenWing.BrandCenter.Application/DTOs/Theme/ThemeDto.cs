namespace HiddenWing.BrandCenter.Application.DTOs.Theme;

public sealed class ThemeDto
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Slug { get; init; }

    public required string PrimaryColor { get; init; }

    public required string SecondaryColor { get; init; }

    public required string AccentColor { get; init; }

    public required string BackgroundColor { get; init; }

    public required string SurfaceColor { get; init; }

    public required string TextColor { get; init; }

    public required string MutedTextColor { get; init; }

    public required string BorderColor { get; init; }

    public required string FontFamily { get; init; }

    public required string BorderRadius { get; init; }

    public bool IsDefault { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; init; }
}
