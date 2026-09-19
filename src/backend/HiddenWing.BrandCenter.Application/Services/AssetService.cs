using HiddenWing.BrandCenter.Application.DTOs.Asset;
using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.Exceptions;
using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Application.Mapping;
using HiddenWing.BrandCenter.Domain.Entities;

namespace HiddenWing.BrandCenter.Application.Services;

public sealed class AssetService : IAssetService
{
    private readonly IAssetRepository _assetRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IProductRepository _productRepository;

    public AssetService(
        IAssetRepository assetRepository,
        IBrandRepository brandRepository,
        IProductRepository productRepository)
    {
        _assetRepository = assetRepository;
        _brandRepository = brandRepository;
        _productRepository = productRepository;
    }

    public async Task<PagedResult<AssetDto>> GetAllAsync(
        int page,
        int pageSize,
        Guid? brandId,
        Guid? productId,
        CancellationToken cancellationToken = default)
    {
        var (safePage, safePageSize) = NormalizePaging(page, pageSize);
        var (items, total) = await _assetRepository.GetPagedAsync(safePage, safePageSize, brandId, productId, cancellationToken);

        return new PagedResult<AssetDto>
        {
            Items = items.Select(EntityMapper.ToDto).ToList(),
            Pagination = new PaginationDto
            {
                Page = safePage,
                PageSize = safePageSize,
                Total = total
            }
        };
    }

    public async Task<AssetDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var asset = await _assetRepository.GetByIdAsync(id, cancellationToken);
        if (asset is null)
        {
            throw new NotFoundException("Asset not found.", "ASSET_NOT_FOUND");
        }

        return EntityMapper.ToDto(asset);
    }

    public async Task<AssetDto> CreateAsync(CreateAssetRequest request, CancellationToken cancellationToken = default)
    {
        await ValidateOwnershipAsync(request.BrandId, request.ProductId, cancellationToken);

        var now = DateTime.UtcNow;
        var asset = new BrandAsset
        {
            Id = Guid.NewGuid(),
            BrandId = request.BrandId,
            ProductId = request.ProductId,
            Name = request.Name.Trim(),
            Type = request.Type,
            Url = request.Url.Trim(),
            MimeType = request.MimeType?.Trim(),
            FileSize = request.FileSize,
            IsActive = request.IsActive,
            CreatedAt = now,
            UpdatedAt = now
        };

        var created = await _assetRepository.CreateAsync(asset, cancellationToken);
        return EntityMapper.ToDto(created);
    }

    public async Task<AssetDto> UpdateAsync(Guid id, UpdateAssetRequest request, CancellationToken cancellationToken = default)
    {
        var asset = await _assetRepository.GetByIdAsync(id, cancellationToken);
        if (asset is null)
        {
            throw new NotFoundException("Asset not found.", "ASSET_NOT_FOUND");
        }

        await ValidateOwnershipAsync(request.BrandId, request.ProductId, cancellationToken);

        asset.BrandId = request.BrandId;
        asset.ProductId = request.ProductId;
        asset.Name = request.Name.Trim();
        asset.Type = request.Type;
        asset.Url = request.Url.Trim();
        asset.MimeType = request.MimeType?.Trim();
        asset.FileSize = request.FileSize;
        asset.IsActive = request.IsActive;
        asset.UpdatedAt = DateTime.UtcNow;

        await _assetRepository.UpdateAsync(asset, cancellationToken);
        return EntityMapper.ToDto(asset);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var asset = await _assetRepository.GetByIdAsync(id, cancellationToken);
        if (asset is null)
        {
            throw new NotFoundException("Asset not found.", "ASSET_NOT_FOUND");
        }

        await _assetRepository.DeleteAsync(asset, cancellationToken);
    }

    private async Task ValidateOwnershipAsync(Guid brandId, Guid? productId, CancellationToken cancellationToken)
    {
        if (!await _brandRepository.ExistsAsync(brandId, cancellationToken))
        {
            throw new NotFoundException("Brand not found.", "BRAND_NOT_FOUND");
        }

        if (productId.HasValue)
        {
            var product = await _productRepository.GetByIdAsync(productId.Value, cancellationToken);
            if (product is null)
            {
                throw new NotFoundException("Product not found.", "PRODUCT_NOT_FOUND");
            }

            if (product.BrandId != brandId)
            {
                throw new ConflictException("The product does not belong to the specified brand.", "ASSET_OWNERSHIP_MISMATCH");
            }
        }
    }

    private static (int Page, int PageSize) NormalizePaging(int page, int pageSize)
    {
        var safePage = page < 1 ? 1 : page;
        var safePageSize = pageSize is < 1 or > 100 ? 20 : pageSize;
        return (safePage, safePageSize);
    }
}
