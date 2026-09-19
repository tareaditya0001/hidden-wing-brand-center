using HiddenWing.BrandCenter.Api.Filters;
using HiddenWing.BrandCenter.Application.DTOs.Brand;
using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiddenWing.BrandCenter.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/brands")]
[ServiceFilter(typeof(ValidationActionFilter))]
public sealed class BrandController : ControllerBase
{
    private readonly IBrandService _brandService;

    public BrandController(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<BrandDto>>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var result = await _brandService.GetAllAsync(page, pageSize, cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<BrandDto>>.Ok(result.Items, result.Pagination));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<BrandDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var brand = await _brandService.GetByIdAsync(id, cancellationToken);
        return Ok(ApiResponse<BrandDto>.Ok(brand));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<BrandDto>>> Create(
        [FromBody] CreateBrandRequest request,
        CancellationToken cancellationToken)
    {
        var brand = await _brandService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = brand.Id }, ApiResponse<BrandDto>.Ok(brand));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<BrandDto>>> Update(
        Guid id,
        [FromBody] UpdateBrandRequest request,
        CancellationToken cancellationToken)
    {
        var brand = await _brandService.UpdateAsync(id, request, cancellationToken);
        return Ok(ApiResponse<BrandDto>.Ok(brand));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _brandService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
