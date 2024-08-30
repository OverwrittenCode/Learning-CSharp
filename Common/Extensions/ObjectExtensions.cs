namespace Common.Extensions;

public static class ObjectExtensions
{
    public static T GetRandomElement<T>(this List<T> array, Range? range = null)
    {
        range ??= Range.All;

        var minValue = range.Value.Start.GetOffset(array.Count);
        var maxValue = range.Value.End.GetOffset(array.Count);

        return array[new Random().Next(minValue, maxValue)];
    }

    public static T GetRandomElement<T>(this T[] array, Range? range = null)
    {
        range ??= Range.All;

        var minValue = range.Value.Start.GetOffset(array.Length);
        var maxValue = range.Value.End.GetOffset(array.Length);

        return array[new Random().Next(minValue, maxValue)];
    }
}
