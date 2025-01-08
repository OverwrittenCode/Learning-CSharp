namespace Common.Utils;

public static class ConsoleUtils
{
    public static void HighlightConsoleLine(string message, ConsoleColor colour, Action<string> provider)
    {
        ConsoleColor originalColour = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        provider(message);
        Console.ForegroundColor = originalColour;
    }

    public static void HighlightConsoleLine(string message, ConsoleColor colour)
    {
        ConsoleColor originalColour = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColour;
    }

    public static bool GetBooleanChoice(string messageBody)
    {
        HighlightConsoleLine($"{messageBody}? (y/n)", ConsoleColor.Magenta);
        return Console.ReadLine()?.Trim().ToLower() is "y" or "yes" or "t" or "true" or "1";
    }

    public static T GetEnumChoice<T>(T[] values, string messageCategory = "") where T : struct, Enum
    {
        if (!String.IsNullOrEmpty(messageCategory))
        {
            messageCategory = $"[{messageCategory.ToUpper()}]: ";
        }

        HighlightConsoleLine($"{messageCategory}Pick your option from the list:", ConsoleColor.Magenta);
        Console.WriteLine();
        ConsoleColor originalColour = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkYellow;

        foreach (T value in values)
        {
            Console.WriteLine($"{Convert.ToInt32(value)} - {value}");
        }

        Console.ForegroundColor = originalColour;
        Console.WriteLine();
        HighlightConsoleLine("Your Choice", ConsoleColor.Magenta);

        T choice;

        while (!Enum.TryParse(Console.ReadLine()?.Trim(), true, out choice) || !Enum.IsDefined(choice))
        {
            HighlightConsoleLine("[ERROR]: Invalid input. Please try again.", ConsoleColor.Red);
        }

        return choice;
    }

    public static T GetEnumChoice<T>(string messageCategory = "") where T : struct, Enum => GetEnumChoice(Enum.GetValues<T>(), messageCategory);
}
