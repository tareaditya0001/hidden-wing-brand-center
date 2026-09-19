namespace HiddenWing.BrandCenter.Application.DTOs.Auth;

public sealed class LoginResponse
{
    public required string AccessToken { get; init; }

    public required string TokenType { get; init; }

    public int ExpiresInMinutes { get; init; }
}
