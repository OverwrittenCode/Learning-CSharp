using System.Globalization;

namespace Common.Extensions;

public static class NumberExtensions
{
    private const string Format = "N0";

    private static readonly NumberFormatInfo Provider = new() { NumberGroupSeparator = " " };

    public static string ToSeparatedDigits(this int value)
    {
        return value.ToString(Format, Provider);
    }

    public static string ToSeparatedDigits(this double value)
    {
        return value.ToString(Format, Provider);
    }

    public static string ToSeparatedDigits(this decimal value)
    {
        return value.ToString(Format, Provider);
    }

    public static string ToSeparatedDigits(this long value)
    {
        return value.ToString(Format, Provider);
    }
}
