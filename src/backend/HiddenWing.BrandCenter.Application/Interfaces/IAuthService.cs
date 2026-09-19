using HiddenWing.BrandCenter.Application.DTOs.Auth;

namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}
