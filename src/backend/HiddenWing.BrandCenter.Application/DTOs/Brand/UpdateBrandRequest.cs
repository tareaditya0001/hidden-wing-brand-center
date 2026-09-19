namespace HiddenWing.BrandCenter.Application.DTOs.Brand;

public sealed class UpdateBrandRequest
{
    public string Name { get; init; } = string.Empty;

    public string Slug { get; init; } = string.Empty;

    public string CompanyName { get; init; } = string.Empty;

    public string? Description { get; init; }

    public bool IsActive { get; init; } = true;
}
