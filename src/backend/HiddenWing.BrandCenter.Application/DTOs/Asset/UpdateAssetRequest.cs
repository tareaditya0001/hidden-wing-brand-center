using HiddenWing.BrandCenter.Domain.Enums;

namespace HiddenWing.BrandCenter.Application.DTOs.Asset;

public sealed class UpdateAssetRequest
{
    public Guid BrandId { get; init; }

    public Guid? ProductId { get; init; }

    public string Name { get; init; } = string.Empty;

    public AssetType Type { get; init; } = AssetType.Other;

    public string Url { get; init; } = string.Empty;

    public string? MimeType { get; init; }

    public long FileSize { get; init; }

    public bool IsActive { get; init; } = true;
}
