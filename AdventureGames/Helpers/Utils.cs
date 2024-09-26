using Common.Utils;

namespace AdventureGames;

internal static class Utils
{
    /// <summary>
    /// <para>
    /// Converts a given singular word to its plural form based on the provided quantity,
    /// and returns a string containing the quantity and the appropriately pluralized word.
    /// </para>
    /// <example>
    /// Examples:
    /// <code>
    /// Utils.PluralizeWithQuantity("question", "s", 1); // Returns "1 question"
    /// Utils.PluralizeWithQuantity("question", "s", 2); // Returns "2 questions"
    /// </code>
    /// </example>
    /// </summary>
    /// <param name="singularWord">The singular form of the word to be pluralized.</param>
    /// <param name="pluralEnding">The suffix to append to the singular word when pluralizing.</param>
    /// <param name="quantity">The number that determines whether the word should be singular or plural.</param>
    /// <returns>
    /// A formatted string containing the <paramref name="quantity"/> and either the singular or plural form
    /// of <paramref name="singularWord"/>, depending on whether <paramref name="quantity"/> is 1.
    /// </returns>

    public static string PluralizeWithQuantity(
        string singularWord,
        string pluralEnding,
        int quantity
    ) => $"{quantity} {singularWord + (quantity == 1 ? "" : pluralEnding)}";

    /// <summary>
    /// Waits for the player to press <see cref="ConsoleKey.Enter"/> to continue the game.
    /// </summary>
    public static void ContinueOnEnter()
    {
        Console.WriteLine();

        ConsoleUtils.HighlightConsoleLine("Press Enter to continue...", Constants.UserNoticeColour);
        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }

        Console.WriteLine();
    }

    /// <summary>
    /// <inheritdoc cref="TypewriterEffect(String)"/>
    /// Displays a message centred on the console window.
    /// </summary>
    /// <inheritdoc cref="TypewriterEffect(String)"/>
    public static void CentreMessage(string message)
    {
        Console.SetCursorPosition((Console.WindowWidth - message.Length) / 2, Console.CursorTop);

        ConsoleUtils.HighlightConsoleLine(message, Constants.UserNoticeColour, TypewriterEffect);
    }

    /// <summary>
    /// <para>Displays text with a typewriter effect.</para>
    /// </summary>
    /// <param name="message">The message to be displayed.</param>
    public static void TypewriterEffect(string message) => TypewriterEffect(message, 1);

    /// <inheritdoc cref="TypewriterEffect(String)"/>
    /// <param name="speed">The speed of the typewriter effect, where higher values are faster.</param>
    /// <param name="lineEnding">The text used in <see cref="Console.WriteLine(String?)"/> after the <paramref name="message"/> is displayed</param>
    public static void TypewriterEffect(string message, double speed, string? lineEnding = null)
    {
        const int CharacterDelay = 30;

        if (speed <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(speed), "Value must be greater than 0");
        }

        int delay = (int)(1 / speed * CharacterDelay);

        var cancellationTokenSource = new CancellationTokenSource();
        var token = cancellationTokenSource.Token;
        var skip = false;

        var thread = new Thread(() =>
        {
            while (!token.IsCancellationRequested)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    skip = true;
                    cancellationTokenSource.Cancel();
                }
            }
        });

        thread.Start();

        for (var i = 0; i < message.Length; i++)
        {
            if (skip)
            {
                Console.Write(message[i..]);
                break;
            }

            Console.Write(message[i]);
            Thread.Sleep(delay);
        }

        Console.WriteLine(lineEnding);
        cancellationTokenSource.Cancel();
        thread.Join();
    }
}
