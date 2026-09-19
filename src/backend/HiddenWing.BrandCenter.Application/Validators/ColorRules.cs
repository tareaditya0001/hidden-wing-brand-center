using System.Text.RegularExpressions;

namespace HiddenWing.BrandCenter.Application.Validators;

public static partial class ColorRules
{
    public const string Pattern = "^#(?:[0-9a-fA-F]{3}|[0-9a-fA-F]{6}|[0-9a-fA-F]{8})$";

    public const string Message = "Color must be a valid hex value such as #1C3353.";

    public static bool IsValid(string? color)
    {
        return string.IsNullOrWhiteSpace(color) || ColorRegex().IsMatch(color);
    }

    [GeneratedRegex(Pattern)]
    private static partial Regex ColorRegex();
}
