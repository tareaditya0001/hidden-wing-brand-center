namespace HiddenWing.BrandCenter.Domain.Seed;

/// <summary>
/// Fixed timestamps required by EF Core HasData seeding.
/// </summary>
public static class SeedDates
{
    public static readonly DateTime CreatedAt = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
}
