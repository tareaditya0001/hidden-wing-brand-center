using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace HiddenWing.BrandCenter.Infrastructure.Storage;

public sealed class AssetStorageService : IAssetStorageService
{
    private readonly StorageSettings _settings;

    public AssetStorageService(IOptions<StorageSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task<StoredAsset> SaveAsync(Stream content, string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        Directory.CreateDirectory(_settings.LocalPath);

        var safeName = $"{Guid.NewGuid():N}-{Path.GetFileName(fileName)}";
        var fullPath = Path.Combine(_settings.LocalPath, safeName);

        await using var fileStream = File.Create(fullPath);
        await content.CopyToAsync(fileStream, cancellationToken);
        var fileSize = fileStream.Length;

        var baseUrl = _settings.BaseUrl.TrimEnd('/');
        return new StoredAsset
        {
            Url = $"{baseUrl}/{safeName}",
            FileName = safeName,
            ContentType = contentType,
            FileSize = fileSize
        };
    }
}
