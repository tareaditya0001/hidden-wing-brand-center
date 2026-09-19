using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.DTOs.Theme;

namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IThemeService
{
    Task<PagedResult<ThemeDto>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default);

    Task<ThemeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<ThemeDto> CreateAsync(CreateThemeRequest request, CancellationToken cancellationToken = default);

    Task<ThemeDto> UpdateAsync(Guid id, UpdateThemeRequest request, CancellationToken cancellationToken = default);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
