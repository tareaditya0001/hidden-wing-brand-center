using HiddenWing.BrandCenter.Application.DTOs.Asset;
using HiddenWing.BrandCenter.Application.DTOs.Common;

namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IAssetService
{
    Task<PagedResult<AssetDto>> GetAllAsync(int page, int pageSize, Guid? brandId, Guid? productId, CancellationToken cancellationToken = default);

    Task<AssetDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<AssetDto> CreateAsync(CreateAssetRequest request, CancellationToken cancellationToken = default);

    Task<AssetDto> UpdateAsync(Guid id, UpdateAssetRequest request, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
