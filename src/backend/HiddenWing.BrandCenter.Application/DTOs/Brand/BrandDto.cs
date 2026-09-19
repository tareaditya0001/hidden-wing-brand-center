namespace HiddenWing.BrandCenter.Application.DTOs.Brand;

public sealed class BrandDto
{
    public Guid Id { get; init; }

    public required string Name { get; init; }

    public required string Slug { get; init; }

    public required string CompanyName { get; init; }

    public string? Description { get; init; }

    public bool IsActive { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; init; }
}
