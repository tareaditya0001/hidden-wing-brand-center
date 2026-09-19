using HiddenWing.BrandCenter.Application.DTOs.Dashboard;

namespace HiddenWing.BrandCenter.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync(CancellationToken cancellationToken = default);
}
