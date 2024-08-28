namespace Common.Extensions;

public static class ObjectExtensions
{
    public static T GetRandomElement<T>(this List<T> array)
    {
        return array[new Random().Next(array.Count)];
    }

    public static T GetRandomElement<T>(this T[] array)
    {
        return array[new Random().Next(array.Length)];
    }
}
