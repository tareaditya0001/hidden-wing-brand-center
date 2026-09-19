using HiddenWing.BrandCenter.Api.Filters;
using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.DTOs.Theme;
using HiddenWing.BrandCenter.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiddenWing.BrandCenter.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/themes")]
[ServiceFilter(typeof(ValidationActionFilter))]
public sealed class ThemeController : ControllerBase
{
    private readonly IThemeService _themeService;

    public ThemeController(IThemeService themeService)
    {
        _themeService = themeService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IReadOnlyList<ThemeDto>>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        var result = await _themeService.GetAllAsync(page, pageSize, cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<ThemeDto>>.Ok(result.Items, result.Pagination));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ApiResponse<ThemeDto>>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var theme = await _themeService.GetByIdAsync(id, cancellationToken);
        return Ok(ApiResponse<ThemeDto>.Ok(theme));
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ThemeDto>>> Create(
        [FromBody] CreateThemeRequest request,
        CancellationToken cancellationToken)
    {
        var theme = await _themeService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = theme.Id }, ApiResponse<ThemeDto>.Ok(theme));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ApiResponse<ThemeDto>>> Update(
        Guid id,
        [FromBody] UpdateThemeRequest request,
        CancellationToken cancellationToken)
    {
        var theme = await _themeService.UpdateAsync(id, request, cancellationToken);
        return Ok(ApiResponse<ThemeDto>.Ok(theme));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await _themeService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}
