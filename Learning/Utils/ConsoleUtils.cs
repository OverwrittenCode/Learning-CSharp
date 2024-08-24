namespace Learning;

internal static class ConsoleUtils
{
    public static void HighlightConsoleLine(string str, ConsoleColor colour)
    {
        var originalColour = Console.ForegroundColor;

        Console.ForegroundColor = colour;

        Console.WriteLine(str);

        Console.ForegroundColor = originalColour;
    }
}
