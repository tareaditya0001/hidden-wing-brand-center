using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using HiddenWing.BrandCenter.Application.DTOs.Auth;
using HiddenWing.BrandCenter.Application.DTOs.Common;

namespace HiddenWing.BrandCenter.IntegrationTests;

public sealed class AdminApiTests : IClassFixture<BrandCenterApiFactory>
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly BrandCenterApiFactory _factory;

    public AdminApiTests(BrandCenterApiFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Products_RequireAuthentication()
    {
        var client = await _factory.CreateReadyClientAsync();

        var response = await client.GetAsync("/api/v1/products");

        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_ThenListProducts_ReturnsSeededCatalog()
    {
        var client = await _factory.CreateReadyClientAsync();
        await AuthenticateAsync(client);

        var response = await client.GetAsync("/api/v1/products");
        var body = await response.Content.ReadAsStringAsync();
        response.StatusCode.Should().Be(HttpStatusCode.OK, because: $"products body: {body}; www-authenticate: {response.Headers.WwwAuthenticate}");

        var payload = await response.Content.ReadFromJsonAsync<ApiResponse<List<ProductListItem>>>(JsonOptions);
        payload.Should().NotBeNull();
        payload!.Success.Should().BeTrue();
        payload.Data.Should().NotBeNull();
        payload.Data!.Select(product => product.Slug).Should().Contain(new[] { "hq", "store", "projects", "data", "admin" });
    }

    [Fact]
    public async Task CreateBrand_RejectsInvalidSlug()
    {
        var client = await _factory.CreateReadyClientAsync();
        await AuthenticateAsync(client);

        var response = await client.PostAsJsonAsync("/api/v1/brands", new
        {
            name = "Broken",
            slug = "Broken Slug",
            companyName = "Broken"
        });

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var body = await response.Content.ReadAsStringAsync();
        body.Should().Contain("VALIDATION_FAILED");
    }

    [Fact]
    public async Task Health_ReturnsHealthy()
    {
        var client = await _factory.CreateReadyClientAsync();

        var response = await client.GetAsync("/health");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private static async Task AuthenticateAsync(HttpClient client)
    {
        var login = await client.PostAsJsonAsync("/api/v1/auth/login", new LoginRequest
        {
            Username = "admin",
            Password = "admin"
        });

        login.StatusCode.Should().Be(HttpStatusCode.OK);
        var raw = await login.Content.ReadAsStringAsync();
        var payload = JsonSerializer.Deserialize<ApiResponse<LoginResponse>>(raw, JsonOptions);
        payload.Should().NotBeNull($"login body was: {raw}");
        payload!.Data.Should().NotBeNull($"login body was: {raw}");
        payload.Data!.AccessToken.Should().NotBeNullOrWhiteSpace($"login body was: {raw}");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", payload.Data.AccessToken);
    }

    private sealed class ProductListItem
    {
        public string Slug { get; set; } = string.Empty;
    }
}
