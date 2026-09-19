using HiddenWing.BrandCenter.Api.Filters;
using HiddenWing.BrandCenter.Application.DTOs.Asset;
using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiddenWing.BrandCenter.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/assets")]
[ServiceFilter(typeof(ValidationActionFilter))]
public sealed class AssetController : ControllerBase
{
    private readonly IAssetService _assetService;
    private readonly IAssetStorageService _assetStorageService;

    public AssetController(IAssetService assetService, IAssetStorageService assetStorageService)
    {
        _assetService = assetService;
        _assetStorageService = assetStorageService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<AssetDto>>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] Guid? brandId = null,
        [FromQuery] Guid? productId = null,
        CancellationToken cancellationToken = default)
    {
        var result = await _assetService.GetAllAsync(page, pageSize, brandId, productId, cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<AssetDto>>.Ok(result.Items, result.Pagination));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<AssetDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var asset = await _assetService.GetByIdAsync(id, cancellationToken);
        return Ok(ApiResponse<AssetDto>.Ok(asset));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<AssetDto>>> Create(
        [FromBody] CreateAssetRequest request,
        CancellationToken cancellationToken)
    {
        var asset = await _assetService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = asset.Id }, ApiResponse<AssetDto>.Ok(asset));
    }

    [HttpPost("upload")]
    [RequestSizeLimit(10_000_000)]
    public async Task<ActionResult<ApiResponse<AssetDto>>> Upload(
        [FromForm] Guid brandId,
        [FromForm] Guid? productId,
        [FromForm] string name,
        [FromForm] AssetType type,
        IFormFile file,
        CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
        {
            return BadRequest(ApiResponse<AssetDto>.Fail("A file is required.", "ASSET_FILE_REQUIRED"));
        }

        await using var stream = file.OpenReadStream();
        var stored = await _assetStorageService.SaveAsync(stream, file.FileName, file.ContentType, cancellationToken);

        var asset = await _assetService.CreateAsync(new CreateAssetRequest
        {
            BrandId = brandId,
            ProductId = productId,
            Name = name,
            Type = type,
            Url = stored.Url,
            MimeType = stored.ContentType,
            FileSize = stored.FileSize,
            IsActive = true
        }, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = asset.Id }, ApiResponse<AssetDto>.Ok(asset));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<AssetDto>>> Update(
        Guid id,
        [FromBody] UpdateAssetRequest request,
        CancellationToken cancellationToken)
    {
        var asset = await _assetService.UpdateAsync(id, request, cancellationToken);
        return Ok(ApiResponse<AssetDto>.Ok(asset));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _assetService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
