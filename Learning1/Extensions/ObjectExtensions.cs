namespace Learning1.Extensions;

internal static class ObjectExtensions
{
    public static T GetRandomElement<T>(this List<T> array)
    {
        var random = new Random();

        return array[random.Next(array.Count)];
    }

    public static T GetRandomElement<T>(this T[] array)
    {
        var random = new Random();

        return array[random.Next(array.Length)];
    }
}
