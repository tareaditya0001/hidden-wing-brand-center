using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using HiddenWing.BrandCenter.Application.DTOs.Auth;
using HiddenWing.BrandCenter.Application.Exceptions;
using HiddenWing.BrandCenter.Application.Interfaces;
using HiddenWing.BrandCenter.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HiddenWing.BrandCenter.Infrastructure.Security;

public sealed class AuthService : IAuthService
{
    private readonly SecuritySettings _settings;

    public AuthService(IOptions<SecuritySettings> settings)
    {
        _settings = settings.Value;
    }

    public Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!FixedTimeEquals(request.Username, _settings.AdminUsername) ||
            !FixedTimeEquals(request.Password, _settings.AdminPassword))
        {
            throw new UnauthorizedAppException("Invalid credentials.", "INVALID_CREDENTIALS");
        }

        var expires = DateTime.UtcNow.AddMinutes(_settings.TokenLifetimeMinutes);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.JwtSecret))
        {
            KeyId = "hidden-wing-brand-center"
        };
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _settings.JwtIssuer,
            audience: _settings.JwtAudience,
            claims:
            [
                new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, "Admin")
            ],
            expires: expires,
            signingCredentials: credentials);

        return Task.FromResult(new LoginResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            TokenType = "Bearer",
            ExpiresInMinutes = _settings.TokenLifetimeMinutes
        });
    }

    private static bool FixedTimeEquals(string left, string right)
    {
        var leftBytes = Encoding.UTF8.GetBytes(left);
        var rightBytes = Encoding.UTF8.GetBytes(right);
        if (leftBytes.Length != rightBytes.Length)
        {
            return false;
        }

        return CryptographicOperations.FixedTimeEquals(leftBytes, rightBytes);
    }
}
