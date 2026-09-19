using HiddenWing.BrandCenter.Domain.Enums;

namespace HiddenWing.BrandCenter.Application.DTOs.Asset;

public sealed class AssetDto
{
    public Guid Id { get; init; }

    public Guid BrandId { get; init; }

    public Guid? ProductId { get; init; }

    public required string Name { get; init; }

    public AssetType Type { get; init; }

    public required string Url { get; init; }

    public string? MimeType { get; init; }

    public long FileSize { get; init; }

    public bool IsActive { get; init; }

    public DateTime CreatedAt { get; init; }

    public DateTime UpdatedAt { get; init; }
}
