namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IAssetStorageService
{
    Task<StoredAsset> SaveAsync(Stream content, string fileName, string contentType, CancellationToken cancellationToken = default);
}

public sealed class StoredAsset
{
    public required string Url { get; init; }

    public required string FileName { get; init; }

    public required string ContentType { get; init; }

    public required long FileSize { get; init; }
}
