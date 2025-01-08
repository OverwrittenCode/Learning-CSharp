namespace Common.Extensions;

public static class ObjectExtensions
{
    private static readonly Random Random = new();

    public static T GetRandomElement<T>(this IList<T> array) => array[Random.Next(array.Count)];
    public static T GetRandomElement<T>(this IList<T> array, Range range) => array[Random.Next(range.Start.GetOffset(array.Count), range.End.GetOffset(array.Count))];
}
