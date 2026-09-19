using HiddenWing.BrandCenter.Domain.Enums;

namespace HiddenWing.BrandCenter.Domain.Entities;

/// <summary>
/// An application in the Hidden Wing ecosystem that can inherit or override branding.
/// </summary>
public sealed class Product : Entity
{
    public Guid BrandId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? ApplicationUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public ProductStatus Status { get; set; } = ProductStatus.Active;

    public Brand Brand { get; set; } = null!;

    public ProductBranding? Branding { get; set; }

    public ICollection<BrandAsset> Assets { get; set; } = new List<BrandAsset>();
}
