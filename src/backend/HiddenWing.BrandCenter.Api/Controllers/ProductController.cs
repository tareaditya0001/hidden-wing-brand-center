using HiddenWing.BrandCenter.Api.Filters;
using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.DTOs.Product;
using HiddenWing.BrandCenter.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiddenWing.BrandCenter.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/products")]
[ServiceFilter(typeof(ValidationActionFilter))]
public sealed class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ProductDto>>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var result = await _productService.GetAllAsync(page, pageSize, cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<ProductDto>>.Ok(result.Items, result.Pagination));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetByIdAsync(id, cancellationToken);
        return Ok(ApiResponse<ProductDto>.Ok(product));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProductDto>>> Create(
        [FromBody] CreateProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _productService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, ApiResponse<ProductDto>.Ok(product));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<ProductDto>>> Update(
        Guid id,
        [FromBody] UpdateProductRequest request,
        CancellationToken cancellationToken)
    {
        var product = await _productService.UpdateAsync(id, request, cancellationToken);
        return Ok(ApiResponse<ProductDto>.Ok(product));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _productService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }

    [HttpGet("{id:guid}/branding")]
    public async Task<ActionResult<ApiResponse<ProductBrandingDto>>> GetBranding(Guid id, CancellationToken cancellationToken)
    {
        var branding = await _productService.GetBrandingAsync(id, cancellationToken);
        return Ok(ApiResponse<ProductBrandingDto>.Ok(branding));
    }

    [HttpPut("{id:guid}/branding")]
    public async Task<ActionResult<ApiResponse<ProductBrandingDto>>> UpdateBranding(
        Guid id,
        [FromBody] UpdateProductBrandingRequest request,
        CancellationToken cancellationToken)
    {
        var branding = await _productService.UpdateBrandingAsync(id, request, cancellationToken);
        return Ok(ApiResponse<ProductBrandingDto>.Ok(branding));
    }
}
