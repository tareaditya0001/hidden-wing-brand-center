using FluentAssertions;
using HiddenWing.BrandCenter.Application.DTOs.Theme;
using HiddenWing.BrandCenter.Application.Exceptions;
using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Application.Services;
using HiddenWing.BrandCenter.Domain.Entities;
using Moq;

namespace HiddenWing.BrandCenter.UnitTests.Services;

public sealed class ThemeServiceTests
{
    private readonly Mock<IThemeRepository> _themeRepository = new();
    private readonly ThemeService _service;

    public ThemeServiceTests()
    {
        _service = new ThemeService(_themeRepository.Object);
    }

    [Fact]
    public async Task DeleteAsync_Throws_WhenThemeIsDefault()
    {
        var themeId = Guid.NewGuid();
        _themeRepository
            .Setup(repository => repository.GetByIdAsync(themeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Theme { Id = themeId, Name = "Default", Slug = "default", IsDefault = true });

        var act = async () => await _service.DeleteAsync(themeId);

        await act.Should().ThrowAsync<ConflictException>().Where(exception => exception.ErrorCode == "DEFAULT_THEME_LOCKED");
    }

    [Fact]
    public async Task CreateAsync_ClearsPreviousDefault()
    {
        _themeRepository
            .Setup(repository => repository.SlugExistsAsync("night", null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _themeRepository
            .Setup(repository => repository.CreateAsync(It.IsAny<Theme>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Theme theme, CancellationToken _) => theme);

        await _service.CreateAsync(ValidRequest(isDefault: true));

        _themeRepository.Verify(
            repository => repository.ClearDefaultAsync(null, It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_Throws_WhenUnsettingTheOnlyDefault()
    {
        var themeId = Guid.NewGuid();
        _themeRepository
            .Setup(repository => repository.GetByIdAsync(themeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Theme { Id = themeId, Name = "Default", Slug = "default", IsDefault = true });
        _themeRepository
            .Setup(repository => repository.SlugExistsAsync("default", themeId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var request = new UpdateThemeRequest
        {
            Name = "Default",
            Slug = "default",
            PrimaryColor = "#111827",
            SecondaryColor = "#1F2937",
            AccentColor = "#C6A15B",
            BackgroundColor = "#0B1220",
            SurfaceColor = "#111827",
            TextColor = "#F9FAFB",
            MutedTextColor = "#9CA3AF",
            BorderColor = "#374151",
            FontFamily = "Inter, system-ui, sans-serif",
            BorderRadius = "8px",
            IsDefault = false
        };
        var act = async () => await _service.UpdateAsync(themeId, request);

        await act.Should().ThrowAsync<ConflictException>().Where(exception => exception.ErrorCode == "DEFAULT_THEME_REQUIRED");
    }

    private static CreateThemeRequest ValidRequest(bool isDefault)
    {
        return new CreateThemeRequest
        {
            Name = "Night",
            Slug = "night",
            PrimaryColor = "#111827",
            SecondaryColor = "#1F2937",
            AccentColor = "#C6A15B",
            BackgroundColor = "#0B1220",
            SurfaceColor = "#111827",
            TextColor = "#F9FAFB",
            MutedTextColor = "#9CA3AF",
            BorderColor = "#374151",
            FontFamily = "Inter, system-ui, sans-serif",
            BorderRadius = "8px",
            IsDefault = isDefault
        };
    }
}
