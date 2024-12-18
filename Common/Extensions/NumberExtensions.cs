using System.Globalization;

namespace Common.Extensions;

public static class NumberExtensions
{
    private const string Format = "N0";

    private static readonly NumberFormatInfo Provider = new()
    {
        NumberGroupSeparator = " "
    };

    public static string ToSeparatedDigits(this IFormattable value)  => value.ToString(Format, Provider);
}
