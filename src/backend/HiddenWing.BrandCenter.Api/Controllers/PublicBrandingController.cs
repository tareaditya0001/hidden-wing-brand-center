using HiddenWing.BrandCenter.Application.DTOs.Branding;
using HiddenWing.BrandCenter.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiddenWing.BrandCenter.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/public/branding")]
public sealed class PublicBrandingController : ControllerBase
{
    private readonly IPublicBrandingService _publicBrandingService;

    public PublicBrandingController(IPublicBrandingService publicBrandingService)
    {
        _publicBrandingService = publicBrandingService;
    }

    [HttpGet("{productSlug}")]
    public async Task<ActionResult<PublicBrandingDto>> GetByProductSlug(
        string productSlug,
        CancellationToken cancellationToken)
    {
        var branding = await _publicBrandingService.GetByProductSlugAsync(productSlug, cancellationToken);
        return Ok(branding);
    }
}
