using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.DTOs.Product;

namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IProductService
{
    Task<PagedResult<ProductDto>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);

    Task<ProductDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default);

    Task<ProductDto> UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    Task<ProductBrandingDto> GetBrandingAsync(Guid productId, CancellationToken cancellationToken = default);

    Task<ProductBrandingDto> UpdateBrandingAsync(Guid productId, UpdateProductBrandingRequest request, CancellationToken cancellationToken = default);
}
