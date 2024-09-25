namespace Common.Utils;

public static class ConsoleUtils
{
    public static void GenerateText(string message)
    {
        for (int i = 0; i < message.Length; i++)
        {
            char letter = message[i];

            if (i == message.Length - 1)
            {
                Console.WriteLine(letter);
            }
            else
            {
                Console.Write(letter);
            }

            Thread.Sleep(20);
        }
    }

    public static void SendMessage(string message, Action<string>? provider = null)
    {
        if (provider is null)
        {
            Console.WriteLine(message);
        }
        else
        {
            provider(message);
        }
    }

    public static void HighlightConsoleLine(
        string message,
        ConsoleColor colour,
        Action<string>? provider = null
    )
    {
        var originalColour = Console.ForegroundColor;

        Console.ForegroundColor = colour;

        SendMessage(message, provider);

        Console.ForegroundColor = originalColour;
    }

    public static bool GetBooleanChoice(string messageBody)
    {
        var message = $"{messageBody}? (y/n)";

        HighlightConsoleLine(message, ConsoleColor.Magenta);

        return Console.ReadLine()?.Trim().ToLower() == "y";
    }

    public static T GetEnumChoice<T>(T[] values, string messageCategory = "")
        where T : struct, Enum
    {
        if (!String.IsNullOrEmpty(messageCategory))
        {
            messageCategory = $"[{messageCategory.ToUpper()}]: ";
        }

        var message = $"{messageCategory}Pick your option from the list:";

        HighlightConsoleLine(message, ConsoleColor.Magenta);

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

        while (
            !Int32.TryParse(Console.ReadLine(), out choice) || !Enum.IsDefined(typeof(T), choice)
        )
        {
            HighlightConsoleLine(
                "[ERROR]: Invalid choice. Please enter a number corresponding to an option.",
                ConsoleColor.Red
            );
        }

        return (T)(object)choice;
    }

    public static T GetEnumChoice<T>(string messageCategory = "")
        where T : struct, Enum
    {
        var values = Enum.GetValues<T>();

        return GetEnumChoice(values, messageCategory);
    }
}
