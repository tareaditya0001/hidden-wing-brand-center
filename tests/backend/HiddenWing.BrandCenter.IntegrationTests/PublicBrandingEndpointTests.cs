using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using HiddenWing.BrandCenter.Application.DTOs.Branding;

namespace HiddenWing.BrandCenter.IntegrationTests;

public sealed class PublicBrandingEndpointTests : IClassFixture<BrandCenterApiFactory>
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly BrandCenterApiFactory _factory;

    public PublicBrandingEndpointTests(BrandCenterApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetStoreBranding_ReturnsResolvedPublicPayload()
    {
        var client = await _factory.CreateReadyClientAsync();

        var response = await client.GetAsync("/api/v1/public/branding/store");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var payload = await response.Content.ReadFromJsonAsync<PublicBrandingDto>(JsonOptions);
        payload.Should().NotBeNull();
        payload!.Company.Name.Should().Be("Hidden Wing");
        payload.Product.Slug.Should().Be("store");
        payload.Product.Name.Should().Be("Hidden Wing Store");
        payload.Theme.PrimaryColor.Should().Be("#1C3353");
        payload.Theme.FontFamily.Should().Contain("Inter");
    }

    [Fact]
    public async Task GetUnknownProduct_ReturnsNotFoundEnvelope()
    {
        var client = await _factory.CreateReadyClientAsync();

        var response = await client.GetAsync("/api/v1/public/branding/missing");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("PUBLIC_BRANDING_NOT_FOUND");
    }
}
