using FluentAssertions;
using HiddenWing.BrandCenter.Application.DTOs.Brand;
using HiddenWing.BrandCenter.Application.Exceptions;
using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Application.Services;
using HiddenWing.BrandCenter.Domain.Entities;
using Moq;

namespace HiddenWing.BrandCenter.UnitTests.Services;

public sealed class BrandServiceTests
{
    private readonly Mock<IBrandRepository> _brandRepository = new();
    private readonly Mock<IProductRepository> _productRepository = new();
    private readonly BrandService _service;

    public BrandServiceTests()
    {
        _service = new BrandService(_brandRepository.Object, _productRepository.Object);
    }

    [Fact]
    public async Task GetByIdAsync_Throws_WhenBrandDoesNotExist()
    {
        _brandRepository
            .Setup(repository => repository.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Brand?)null);

        var act = async () => await _service.GetByIdAsync(Guid.NewGuid());

        await act.Should().ThrowAsync<NotFoundException>().Where(exception => exception.ErrorCode == "BRAND_NOT_FOUND");
    }

    [Fact]
    public async Task CreateAsync_Throws_WhenSlugExists()
    {
        _brandRepository
            .Setup(repository => repository.SlugExistsAsync("hidden-wing", null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var act = async () => await _service.CreateAsync(new CreateBrandRequest
        {
            Name = "Hidden Wing",
            Slug = "hidden-wing",
            CompanyName = "Hidden Wing"
        });

        await act.Should().ThrowAsync<ConflictException>().Where(exception => exception.ErrorCode == "BRAND_SLUG_EXISTS");
    }

    [Fact]
    public async Task CreateAsync_PersistsBrand()
    {
        _brandRepository
            .Setup(repository => repository.SlugExistsAsync("hidden-wing", null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _brandRepository
            .Setup(repository => repository.CreateAsync(It.IsAny<Brand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Brand brand, CancellationToken _) => brand);

        var result = await _service.CreateAsync(new CreateBrandRequest
        {
            Name = "Hidden Wing",
            Slug = "hidden-wing",
            CompanyName = "Hidden Wing",
            Description = "Master brand"
        });

        result.Name.Should().Be("Hidden Wing");
        result.Slug.Should().Be("hidden-wing");
        _brandRepository.Verify(repository => repository.CreateAsync(It.IsAny<Brand>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task DeleteAsync_Throws_WhenProductsExist()
    {
        var brandId = Guid.NewGuid();
        _brandRepository
            .Setup(repository => repository.GetByIdAsync(brandId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Brand { Id = brandId, Name = "Hidden Wing", Slug = "hidden-wing" });
        _productRepository
            .Setup(repository => repository.CountByBrandIdAsync(brandId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(2);

        var act = async () => await _service.DeleteAsync(brandId);

        await act.Should().ThrowAsync<ConflictException>().Where(exception => exception.ErrorCode == "BRAND_HAS_PRODUCTS");
    }
}
