using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.DTOs.Product;
using HiddenWing.BrandCenter.Application.Exceptions;
using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Application.Mapping;
using HiddenWing.BrandCenter.Domain.Entities;

namespace HiddenWing.BrandCenter.Application.Services;

public sealed class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly IThemeRepository _themeRepository;

    public ProductService(
        IProductRepository productRepository,
        IBrandRepository brandRepository,
        IThemeRepository themeRepository)
    {
        _productRepository = productRepository;
        _brandRepository = brandRepository;
        _themeRepository = themeRepository;
    }

    public async Task<PagedResult<ProductDto>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var (safePage, safePageSize) = NormalizePaging(page, pageSize);
        var (items, total) = await _productRepository.GetPagedAsync(safePage, safePageSize, cancellationToken);

        return new PagedResult<ProductDto>
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

    public async Task<ProductDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await RequireProductAsync(id, cancellationToken);
        return EntityMapper.ToDto(product);
    }

    public async Task<ProductDto> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        if (!await _brandRepository.ExistsAsync(request.BrandId, cancellationToken))
        {
            throw new NotFoundException("Brand not found.", "BRAND_NOT_FOUND");
        }

        if (await _productRepository.SlugExistsAsync(request.Slug, excludeId: null, cancellationToken))
        {
            throw new ConflictException("A product with this slug already exists.", "PRODUCT_SLUG_EXISTS");
        }

        var now = DateTime.UtcNow;
        var product = new Product
        {
            Id = Guid.NewGuid(),
            BrandId = request.BrandId,
            Name = request.Name.Trim(),
            Slug = request.Slug.Trim(),
            Description = request.Description?.Trim(),
            ApplicationUrl = request.ApplicationUrl?.Trim(),
            IsActive = request.IsActive,
            Status = EntityMapper.ToStatus(request.IsActive),
            CreatedAt = now,
            UpdatedAt = now
        };

        var created = await _productRepository.CreateAsync(product, cancellationToken);
        var loaded = await RequireProductAsync(created.Id, cancellationToken);
        return EntityMapper.ToDto(loaded);
    }

    public async Task<ProductDto> UpdateAsync(Guid id, UpdateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = await RequireProductAsync(id, cancellationToken);

        if (!await _brandRepository.ExistsAsync(request.BrandId, cancellationToken))
        {
            throw new NotFoundException("Brand not found.", "BRAND_NOT_FOUND");
        }

        if (await _productRepository.SlugExistsAsync(request.Slug, id, cancellationToken))
        {
            throw new ConflictException("A product with this slug already exists.", "PRODUCT_SLUG_EXISTS");
        }

        product.BrandId = request.BrandId;
        product.Name = request.Name.Trim();
        product.Slug = request.Slug.Trim();
        product.Description = request.Description?.Trim();
        product.ApplicationUrl = request.ApplicationUrl?.Trim();
        product.IsActive = request.IsActive;
        product.Status = EntityMapper.ToStatus(request.IsActive);
        product.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateAsync(product, cancellationToken);
        var loaded = await RequireProductAsync(id, cancellationToken);
        return EntityMapper.ToDto(loaded);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var product = await RequireProductAsync(id, cancellationToken);
        await _productRepository.DeleteAsync(product, cancellationToken);
    }

    public async Task<ProductBrandingDto> GetBrandingAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        await RequireProductAsync(productId, cancellationToken);

        var branding = await _productRepository.GetBrandingByProductIdAsync(productId, cancellationToken);
        if (branding is null)
        {
            return new ProductBrandingDto
            {
                Id = Guid.Empty,
                ProductId = productId,
                IsEnabled = true
            };
        }

        return EntityMapper.ToDto(branding);
    }

    public async Task<ProductBrandingDto> UpdateBrandingAsync(
        Guid productId,
        UpdateProductBrandingRequest request,
        CancellationToken cancellationToken = default)
    {
        await RequireProductAsync(productId, cancellationToken);

        if (request.ThemeId.HasValue)
        {
            var theme = await _themeRepository.GetByIdAsync(request.ThemeId.Value, cancellationToken);
            if (theme is null)
            {
                throw new NotFoundException("Theme not found.", "THEME_NOT_FOUND");
            }
        }

        var now = DateTime.UtcNow;
        var branding = await _productRepository.GetBrandingByProductIdAsync(productId, cancellationToken)
            ?? new ProductBranding
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                CreatedAt = now
            };

        branding.ThemeId = request.ThemeId;
        branding.DisplayName = Normalize(request.DisplayName);
        branding.ShortName = Normalize(request.ShortName);
        branding.Tagline = Normalize(request.Tagline);
        branding.LogoUrl = Normalize(request.LogoUrl);
        branding.LogoDarkUrl = Normalize(request.LogoDarkUrl);
        branding.LogoLightUrl = Normalize(request.LogoLightUrl);
        branding.IconUrl = Normalize(request.IconUrl);
        branding.FaviconUrl = Normalize(request.FaviconUrl);
        branding.PrimaryColor = Normalize(request.PrimaryColor);
        branding.SecondaryColor = Normalize(request.SecondaryColor);
        branding.AccentColor = Normalize(request.AccentColor);
        branding.BackgroundColor = Normalize(request.BackgroundColor);
        branding.SurfaceColor = Normalize(request.SurfaceColor);
        branding.TextColor = Normalize(request.TextColor);
        branding.MutedTextColor = Normalize(request.MutedTextColor);
        branding.BorderColor = Normalize(request.BorderColor);
        branding.FontFamily = Normalize(request.FontFamily);
        branding.SupportEmail = Normalize(request.SupportEmail);
        branding.WebsiteUrl = Normalize(request.WebsiteUrl);
        branding.PrivacyUrl = Normalize(request.PrivacyUrl);
        branding.TermsUrl = Normalize(request.TermsUrl);
        branding.IsEnabled = request.IsEnabled;
        branding.UpdatedAt = now;

        var saved = await _productRepository.UpsertBrandingAsync(branding, cancellationToken);
        return EntityMapper.ToDto(saved);
    }

    private async Task<Product> RequireProductAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(id, cancellationToken);
        if (product is null)
        {
            throw new NotFoundException("Product not found.", "PRODUCT_NOT_FOUND");
        }

        return product;
    }

    private static string? Normalize(string? value)
    {
        return string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    private static (int Page, int PageSize) NormalizePaging(int page, int pageSize)
    {
        var safePage = page < 1 ? 1 : page;
        var safePageSize = pageSize is < 1 or > 100 ? 20 : pageSize;
        return (safePage, safePageSize);
    }
}
