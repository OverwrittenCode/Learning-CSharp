namespace Learning;

internal static class ConsoleUtils
{
    public static void HighlightConsoleLine(string value, ConsoleColor colour)
    {
        var originalColour = Console.ForegroundColor;

        Console.ForegroundColor = colour;

        Console.WriteLine(value);

        Console.ForegroundColor = originalColour;
    }
}
