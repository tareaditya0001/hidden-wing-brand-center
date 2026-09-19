using HiddenWing.BrandCenter.Application.DTOs.Brand;
using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.Exceptions;
using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Application.Mapping;
using HiddenWing.BrandCenter.Domain.Entities;

namespace HiddenWing.BrandCenter.Application.Services;

public sealed class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IProductRepository _productRepository;

    public BrandService(IBrandRepository brandRepository, IProductRepository productRepository)
    {
        _brandRepository = brandRepository;
        _productRepository = productRepository;
    }

    public async Task<PagedResult<BrandDto>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var (safePage, safePageSize) = NormalizePaging(page, pageSize);
        var (items, total) = await _brandRepository.GetPagedAsync(safePage, safePageSize, cancellationToken);

        return new PagedResult<BrandDto>
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

    public async Task<BrandDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var brand = await _brandRepository.GetByIdAsync(id, cancellationToken);
        if (brand is null)
        {
            throw new NotFoundException("Brand not found.", "BRAND_NOT_FOUND");
        }

        return EntityMapper.ToDto(brand);
    }

    public async Task<BrandDto> CreateAsync(CreateBrandRequest request, CancellationToken cancellationToken = default)
    {
        if (await _brandRepository.SlugExistsAsync(request.Slug, excludeId: null, cancellationToken))
        {
            throw new ConflictException("A brand with this slug already exists.", "BRAND_SLUG_EXISTS");
        }

        var now = DateTime.UtcNow;
        var brand = new Brand
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Slug = request.Slug.Trim(),
            CompanyName = request.CompanyName.Trim(),
            Description = request.Description?.Trim(),
            IsActive = request.IsActive,
            CreatedAt = now,
            UpdatedAt = now
        };

        var created = await _brandRepository.CreateAsync(brand, cancellationToken);
        return EntityMapper.ToDto(created);
    }

    public async Task<BrandDto> UpdateAsync(Guid id, UpdateBrandRequest request, CancellationToken cancellationToken = default)
    {
        var brand = await _brandRepository.GetByIdAsync(id, cancellationToken);
        if (brand is null)
        {
            throw new NotFoundException("Brand not found.", "BRAND_NOT_FOUND");
        }

        if (await _brandRepository.SlugExistsAsync(request.Slug, id, cancellationToken))
        {
            throw new ConflictException("A brand with this slug already exists.", "BRAND_SLUG_EXISTS");
        }

        brand.Name = request.Name.Trim();
        brand.Slug = request.Slug.Trim();
        brand.CompanyName = request.CompanyName.Trim();
        brand.Description = request.Description?.Trim();
        brand.IsActive = request.IsActive;
        brand.UpdatedAt = DateTime.UtcNow;

        await _brandRepository.UpdateAsync(brand, cancellationToken);
        return EntityMapper.ToDto(brand);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var brand = await _brandRepository.GetByIdAsync(id, cancellationToken);
        if (brand is null)
        {
            throw new NotFoundException("Brand not found.", "BRAND_NOT_FOUND");
        }

        var productCount = await _productRepository.CountByBrandIdAsync(id, cancellationToken);
        if (productCount > 0)
        {
            throw new ConflictException("Cannot delete a brand that still has products.", "BRAND_HAS_PRODUCTS");
        }

        await _brandRepository.DeleteAsync(brand, cancellationToken);
    }

    private static (int Page, int PageSize) NormalizePaging(int page, int pageSize)
    {
        var safePage = page < 1 ? 1 : page;
        var safePageSize = pageSize is < 1 or > 100 ? 20 : pageSize;
        return (safePage, safePageSize);
    }
}
