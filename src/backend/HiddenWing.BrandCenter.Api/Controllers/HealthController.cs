using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HiddenWing.BrandCenter.Api.Controllers;

[ApiController]
[AllowAnonymous]
public sealed class HealthController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    public HealthController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet("/health")]
    public IActionResult Get()
    {
        return Ok(new { status = "Healthy" });
    }

    [HttpGet("/health/ready")]
    public async Task<IActionResult> Ready(CancellationToken cancellationToken)
    {
        var report = await _healthCheckService.CheckHealthAsync(cancellationToken);
        var payload = new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString()
            })
        };

        return report.Status == HealthStatus.Healthy
            ? Ok(payload)
            : StatusCode(StatusCodes.Status503ServiceUnavailable, payload);
    }
}
