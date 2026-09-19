namespace HiddenWing.BrandCenter.Application.DTOs.Dashboard;

public sealed class DashboardSummaryDto
{
    public int ActiveBrands { get; init; }

    public int ActiveProducts { get; init; }

    public int Themes { get; init; }

    public int Assets { get; init; }

    public required IReadOnlyList<RecentChangeDto> RecentChanges { get; init; }
}

public sealed class RecentChangeDto
{
    public required string EntityType { get; init; }

    public required string Name { get; init; }

    public DateTime UpdatedAt { get; init; }
}
