using FluentAssertions;
using HiddenWing.BrandCenter.Application.DTOs.Product;
using HiddenWing.BrandCenter.Application.Exceptions;
using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Application.Services;
using HiddenWing.BrandCenter.Domain.Entities;
using Moq;

namespace HiddenWing.BrandCenter.UnitTests.Services;

public sealed class ProductServiceTests
{
    private readonly Mock<IProductRepository> _productRepository = new();
    private readonly Mock<IBrandRepository> _brandRepository = new();
    private readonly Mock<IThemeRepository> _themeRepository = new();
    private readonly ProductService _service;

    public ProductServiceTests()
    {
        _service = new ProductService(_productRepository.Object, _brandRepository.Object, _themeRepository.Object);
    }

    [Fact]
    public async Task CreateAsync_Throws_WhenBrandIsMissing()
    {
        _brandRepository
            .Setup(repository => repository.ExistsAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var act = async () => await _service.CreateAsync(new CreateProductRequest
        {
            BrandId = Guid.NewGuid(),
            Name = "Hidden Wing Store",
            Slug = "store"
        });

        await act.Should().ThrowAsync<NotFoundException>().Where(exception => exception.ErrorCode == "BRAND_NOT_FOUND");
    }

    [Fact]
    public async Task UpdateBrandingAsync_CreatesOverrideRecord()
    {
        var productId = Guid.NewGuid();
        var brandId = Guid.NewGuid();

        _productRepository
            .Setup(repository => repository.GetByIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Product
            {
                Id = productId,
                BrandId = brandId,
                Name = "Hidden Wing Store",
                Slug = "store",
                Brand = new Brand { Id = brandId, Name = "Hidden Wing" }
            });
        _productRepository
            .Setup(repository => repository.GetBrandingByProductIdAsync(productId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductBranding?)null);
        _productRepository
            .Setup(repository => repository.UpsertBrandingAsync(It.IsAny<ProductBranding>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductBranding branding, CancellationToken _) => branding);

        var result = await _service.UpdateBrandingAsync(productId, new UpdateProductBrandingRequest
        {
            DisplayName = "Hidden Wing Store",
            PrimaryColor = "#6B3FA0"
        });

        result.DisplayName.Should().Be("Hidden Wing Store");
        result.PrimaryColor.Should().Be("#6B3FA0");
    }

    [Fact]
    public async Task GetByIdAsync_Throws_WhenMissing()
    {
        _productRepository
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        var act = async () => await _service.GetByIdAsync(Guid.NewGuid());

        await act.Should().ThrowAsync<NotFoundException>().Where(exception => exception.ErrorCode == "PRODUCT_NOT_FOUND");
    }
}
