namespace Learning.Utils;

internal static class ObjectUtils
{
    public static T GetRandomElement<T>(List<T> array)
    {
        var random = new Random();

        return array[random.Next(array.Count)];
    }

    public static T GetRandomElement<T>(T[] array)
    {
        var random = new Random();

        return array[random.Next(array.Length)];
    }
}
