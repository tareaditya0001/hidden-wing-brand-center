using System.Text.RegularExpressions;

namespace HiddenWing.BrandCenter.Application.Validators;

public static partial class SlugRules
{
    public const string Pattern = "^[a-z0-9]+(?:-[a-z0-9]+)*$";

    public const string Message = "Slug must be lowercase letters, numbers, and hyphens.";

    public static bool IsValid(string? slug)
    {
        return !string.IsNullOrWhiteSpace(slug) && SlugRegex().IsMatch(slug);
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex SlugRegex();
}
