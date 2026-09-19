using HiddenWing.BrandCenter.Domain.Enums;

namespace HiddenWing.BrandCenter.Application.DTOs.Product;

public sealed class ProductDto
{
    public Guid Id { get; init; }

    public Guid BrandId { get; init; }

    public required string BrandName { get; init; }

    public required string Name { get; init; }

    public required string Slug { get; init; }

    public string? Description { get; init; }

    public string? ApplicationUrl { get; init; }

    public bool IsActive { get; init; }

    public ProductStatus Status { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; init; }
}
