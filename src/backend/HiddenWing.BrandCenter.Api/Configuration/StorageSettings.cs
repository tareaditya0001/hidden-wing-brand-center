namespace HiddenWing.BrandCenter.Api.Configuration;

public sealed class StorageSettings
{
    public const string SectionName = "Storage";

    public string BaseUrl { get; set; } = "/uploads";

    public string LocalPath { get; set; } = "storage/uploads";
}
