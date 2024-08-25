namespace Learning.Utils;

internal static class ConsoleUtils
{
    public static void HighlightConsoleLine(string value, ConsoleColor colour)
    {
        var originalColour = Console.ForegroundColor;

        Console.ForegroundColor = colour;

        Console.WriteLine(value);

        Console.ForegroundColor = originalColour;
    }

    public static T GetEnumChoice<T>(T[] values)
        where T : struct, Enum
    {
        HighlightConsoleLine("[TURN]: Pick your option from the list:", ConsoleColor.Magenta);

        Console.WriteLine();

        var originalColour = Console.ForegroundColor;

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        foreach (T value in values)
        {
            Console.WriteLine($"{Convert.ToInt32(value)} - {value}");
        }

        Console.ForegroundColor = originalColour;

        Console.WriteLine();

        HighlightConsoleLine("Your Choice", ConsoleColor.Magenta);

        int choice;

        while (!int.TryParse(Console.ReadLine(), out choice) || !Enum.IsDefined(typeof(T), choice))
        {
            HighlightConsoleLine(
                "[ERROR]: Invalid choice. Please enter a number corresponding to an option.",
                ConsoleColor.Red
            );
        }

        return (T)(object)choice;
    }

    public static T GetEnumChoice<T>()
        where T : struct, Enum
    {
        var values = Enum.GetValues<T>();

        return GetEnumChoice(values);
    }
}
