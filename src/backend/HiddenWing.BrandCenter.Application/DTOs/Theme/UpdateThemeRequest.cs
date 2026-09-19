namespace HiddenWing.BrandCenter.Application.DTOs.Theme;

public sealed record UpdateThemeRequest
{
    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string PrimaryColor { get; init; } = string.Empty;

    public string SecondaryColor { get; init; } = string.Empty;

    public string AccentColor { get; init; } = string.Empty;

    public string BackgroundColor { get; init; } = string.Empty;

    public string SurfaceColor { get; init; } = string.Empty;

    public string TextColor { get; init; } = string.Empty;

    public string MutedTextColor { get; init; } = string.Empty;

    public string BorderColor { get; init; } = string.Empty;

    public string FontFamily { get; init; } = "Inter, system-ui, sans-serif";

    public string BorderRadius { get; init; } = "8px";

    public bool IsDefault { get; init; }
}
