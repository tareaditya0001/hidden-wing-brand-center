using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.DTOs.Theme;
using HiddenWing.BrandCenter.Application.Exceptions;
using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Application.Mapping;
using HiddenWing.BrandCenter.Domain.Entities;

namespace HiddenWing.BrandCenter.Application.Services;

public sealed class ThemeService : IThemeService
{
    private readonly IThemeRepository _themeRepository;

    public ThemeService(IThemeRepository themeRepository)
    {
        _themeRepository = themeRepository;
    }

    public async Task<PagedResult<ThemeDto>> GetAllAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var (safePage, safePageSize) = NormalizePaging(page, pageSize);
        var (items, total) = await _themeRepository.GetPagedAsync(safePage, safePageSize, cancellationToken);

        return new PagedResult<ThemeDto>
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

    public async Task<ThemeDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var theme = await _themeRepository.GetByIdAsync(id, cancellationToken);
        if (theme is null)
        {
            throw new NotFoundException("Theme not found.", "THEME_NOT_FOUND");
        }

        return EntityMapper.ToDto(theme);
    }

    public async Task<ThemeDto> CreateAsync(CreateThemeRequest request, CancellationToken cancellationToken = default)
    {
        if (await _themeRepository.SlugExistsAsync(request.Slug, excludeId: null, cancellationToken))
        {
            throw new ConflictException("A theme with this slug already exists.", "THEME_SLUG_EXISTS");
        }

        if (request.IsDefault)
        {
            await _themeRepository.ClearDefaultAsync(exceptThemeId: null, cancellationToken);
        }

        var now = DateTime.UtcNow;
        var theme = new Theme
        {
            Id = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Slug = request.Slug.Trim(),
            PrimaryColor = request.PrimaryColor.Trim(),
            SecondaryColor = request.SecondaryColor.Trim(),
            AccentColor = request.AccentColor.Trim(),
            BackgroundColor = request.BackgroundColor.Trim(),
            SurfaceColor = request.SurfaceColor.Trim(),
            TextColor = request.TextColor.Trim(),
            MutedTextColor = request.MutedTextColor.Trim(),
            BorderColor = request.BorderColor.Trim(),
            FontFamily = request.FontFamily.Trim(),
            BorderRadius = request.BorderRadius.Trim(),
            IsDefault = request.IsDefault,
            CreatedAt = now,
            UpdatedAt = now
        };

        var created = await _themeRepository.CreateAsync(theme, cancellationToken);
        return EntityMapper.ToDto(created);
    }

    public async Task<ThemeDto> UpdateAsync(Guid id, UpdateThemeRequest request, CancellationToken cancellationToken = default)
    {
        var theme = await _themeRepository.GetByIdAsync(id, cancellationToken);
        if (theme is null)
        {
            throw new NotFoundException("Theme not found.", "THEME_NOT_FOUND");
        }

        if (await _themeRepository.SlugExistsAsync(request.Slug, id, cancellationToken))
        {
            throw new ConflictException("A theme with this slug already exists.", "THEME_SLUG_EXISTS");
        }

        if (theme.IsDefault && !request.IsDefault)
        {
            throw new ConflictException("A default theme must remain assigned. Mark another theme as default first.", "DEFAULT_THEME_REQUIRED");
        }

        if (request.IsDefault)
        {
            await _themeRepository.ClearDefaultAsync(id, cancellationToken);
        }

        theme.Name = request.Name.Trim();
        theme.Slug = request.Slug.Trim();
        theme.PrimaryColor = request.PrimaryColor.Trim();
        theme.SecondaryColor = request.SecondaryColor.Trim();
        theme.AccentColor = request.AccentColor.Trim();
        theme.BackgroundColor = request.BackgroundColor.Trim();
        theme.SurfaceColor = request.SurfaceColor.Trim();
        theme.TextColor = request.TextColor.Trim();
        theme.MutedTextColor = request.MutedTextColor.Trim();
        theme.BorderColor = request.BorderColor.Trim();
        theme.FontFamily = request.FontFamily.Trim();
        theme.BorderRadius = request.BorderRadius.Trim();
        theme.IsDefault = request.IsDefault;
        theme.UpdatedAt = DateTime.UtcNow;

        await _themeRepository.UpdateAsync(theme, cancellationToken);
        return EntityMapper.ToDto(theme);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var theme = await _themeRepository.GetByIdAsync(id, cancellationToken);
        if (theme is null)
        {
            throw new NotFoundException("Theme not found.", "THEME_NOT_FOUND");
        }

        if (theme.IsDefault)
        {
            throw new ConflictException("The default theme cannot be deleted.", "DEFAULT_THEME_LOCKED");
        }

        await _themeRepository.DeleteAsync(theme, cancellationToken);
    }

    private static (int Page, int PageSize) NormalizePaging(int page, int pageSize)
    {
        var safePage = page < 1 ? 1 : page;
        var safePageSize = pageSize is < 1 or > 100 ? 20 : pageSize;
        return (safePage, safePageSize);
    }
}
