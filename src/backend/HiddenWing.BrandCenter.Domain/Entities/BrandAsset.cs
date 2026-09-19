using HiddenWing.BrandCenter.Domain.Enums;

namespace HiddenWing.BrandCenter.Domain.Entities;

/// <summary>
/// A brand file that belongs to the company brand and optionally to one product.
/// </summary>
public sealed class BrandAsset : Entity
{
    public Guid BrandId { get; set; }

    public Guid? ProductId { get; set; }

    public string Name { get; set; } = string.Empty;

    public AssetType Type { get; set; } = AssetType.Other;

    public string Url { get; set; } = string.Empty;

    public string? MimeType { get; set; }

    public long FileSize { get; set; }

    public bool IsActive { get; set; } = true;

    public Brand Brand { get; set; } = null!;

    public Product? Product { get; set; }
}
