namespace Common.Extensions;

public static class ObjectExtensions
{
    public static T GetRandomElement<T>(this IList<T> array) => array[new Random().Next(0, array.Count)];
    public static T GetRandomElement<T>(this IList<T> array, Range range) => array[new Random().Next(range.Start.GetOffset(array.Count), range.End.GetOffset(array.Count))];
}
