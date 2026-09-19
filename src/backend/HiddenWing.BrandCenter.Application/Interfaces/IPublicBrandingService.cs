using HiddenWing.BrandCenter.Application.DTOs.Branding;

namespace HiddenWing.BrandCenter.Application.Interfaces;

/// <summary>
/// Resolves inherited branding for consumption by other Hidden Wing applications.
/// Designed so a cache can be introduced later without changing controllers.
/// </summary>
public interface IPublicBrandingService
{
    Task<PublicBrandingDto> GetByProductSlugAsync(string productSlug, CancellationToken cancellationToken = default);
}
