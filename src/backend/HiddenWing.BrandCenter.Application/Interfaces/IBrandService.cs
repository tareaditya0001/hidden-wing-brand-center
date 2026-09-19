using HiddenWing.BrandCenter.Application.DTOs.Brand;
using HiddenWing.BrandCenter.Application.DTOs.Common;

namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IBrandService
{
    Task<PagedResult<BrandDto>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);

    Task<BrandDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<BrandDto> CreateAsync(CreateBrandRequest request, CancellationToken cancellationToken = default);

    Task<BrandDto> UpdateAsync(Guid id, UpdateBrandRequest request, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
