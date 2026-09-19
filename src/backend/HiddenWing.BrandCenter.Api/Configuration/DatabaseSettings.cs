namespace HiddenWing.BrandCenter.Api.Configuration;

public sealed class DatabaseSettings
{
    public const string SectionName = "Database";

    public string DefaultConnection { get; set; } = string.Empty;
}
