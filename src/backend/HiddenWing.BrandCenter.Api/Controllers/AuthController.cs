using HiddenWing.BrandCenter.Api.Filters;
using HiddenWing.BrandCenter.Application.DTOs.Auth;
using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiddenWing.BrandCenter.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/v1/auth")]
[ServiceFilter(typeof(ValidationActionFilter))]
public sealed class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<LoginResponse>>> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);
        return Ok(ApiResponse<LoginResponse>.Ok(result));
    }
}
