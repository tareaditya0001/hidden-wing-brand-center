using HiddenWing.BrandCenter.Application.DTOs.Common;
using HiddenWing.BrandCenter.Application.DTOs.Dashboard;
using HiddenWing.BrandCenter.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HiddenWing.BrandCenter.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/dashboard")]
public sealed class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<DashboardSummaryDto>>> Get(CancellationToken cancellationToken)
    {
        var summary = await _dashboardService.GetSummaryAsync(cancellationToken);
        return Ok(ApiResponse<DashboardSummaryDto>.Ok(summary));
    }
}
