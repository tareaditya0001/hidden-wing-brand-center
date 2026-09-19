namespace HiddenWing.BrandCenter.Domain.Entities;

/// <summary>
/// Company-level brand identity used as the default source of truth.
/// </summary>
public sealed class Brand : Entity
{
    public string Name { get; set; } = string.Empty;

    public string Slug { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public ICollection<Product> Products { get; set; } = new List<Product>();

    public ICollection<BrandAsset> Assets { get; set; } = new List<BrandAsset>();
}
