namespace HiddenWing.BrandCenter.Domain.Entities;

/// <summary>
/// Reusable color and typography palette that products can inherit.
/// </summary>
public sealed class Theme : Entity
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string PrimaryColor { get; set; } = string.Empty;

    public string SecondaryColor { get; set; } = string.Empty;

    public string AccentColor { get; set; } = string.Empty;

    public string BackgroundColor { get; set; } = string.Empty;

    public string SurfaceColor { get; set; } = string.Empty;

    public string TextColor { get; set; } = string.Empty;

    public string MutedTextColor { get; set; } = string.Empty;

    public string BorderColor { get; set; } = string.Empty;

    public string FontFamily { get; set; } = string.Empty;

    public string BorderRadius { get; set; } = "8px";

    public bool IsDefault { get; set; }

    public ICollection<ProductBranding> ProductBrandings { get; set; } = new List<ProductBranding>();
}
