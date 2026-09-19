namespace HiddenWing.BrandCenter.Application.DTOs.Product;

public sealed class CreateProductRequest
{
    public Guid BrandId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string? Description { get; init; }

    public string? ApplicationUrl { get; init; }

    public bool IsActive { get; init; } = true;
}
