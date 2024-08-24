using System.ComponentModel;
using Learning;

const int MinRequiredWins = 0;
const int MaxRequiredWins = 8;

var AllowedRangeNotice = $"({MinRequiredWins} - {MaxRequiredWins})";

GameType[] games = Enum.GetValues<GameType>();

ConsoleUtils.HighlightConsoleLine("[TURN]: Pick your option from the list:", ConsoleColor.Magenta);

Console.WriteLine();

Console.ForegroundColor = ConsoleColor.DarkYellow;

foreach (GameType game in games)
{
    Console.WriteLine($"{(int)game} - {game}");
}

Console.ResetColor();

Console.WriteLine();

ConsoleUtils.HighlightConsoleLine("Your Choice", ConsoleColor.Magenta);

int choice;

Console.WriteLine();

while (!int.TryParse(Console.ReadLine(), out choice) || !Enum.IsDefined(typeof(GameType), choice))
{
    ConsoleUtils.HighlightConsoleLine(
        "[ERROR]: Invalid choice. Please enter a number corresponding to an option.",
        ConsoleColor.Red
    );
}

var userChoice = (GameType)choice;

ConsoleUtils.HighlightConsoleLine(
    $"----- [{userChoice.ToString().ToUpper()} GAME] -----",
    ConsoleColor.Cyan
);

while (true)
{
    ConsoleUtils.HighlightConsoleLine(
        $"[CONFIG]: How many wins are required to end the game {AllowedRangeNotice}?",
        ConsoleColor.Magenta
    );

    int requiredWins;

    Console.WriteLine();

    while (
        !int.TryParse(Console.ReadLine(), out requiredWins)
        || requiredWins <= MinRequiredWins
        || requiredWins >= MaxRequiredWins
    )
    {
        ConsoleUtils.HighlightConsoleLine(
            $"[ERROR]: Please enter a valid integer {AllowedRangeNotice}.",
            ConsoleColor.Red
        );
    }

    {
        var enableDeuce = false;

        if (requiredWins > 2)
        {
            Console.WriteLine();

            ConsoleUtils.HighlightConsoleLine(
                "[CONFIG]: Enable deuce mode? (y/n)",
                ConsoleColor.Magenta
            );

            var deuceInput = Console.ReadLine();

            enableDeuce = deuceInput?.Trim().ToLower() == "y";
        }

        switch (userChoice)
        {
            case GameType.Rps:
                new Learning.RPS.Game(requiredWins, enableDeuce).Init();

                break;
            case GameType.TicTacToe:
                new Learning.TicTacToe.Game(requiredWins, enableDeuce).Init();

                break;
            default:
                throw new InvalidEnumArgumentException($"Unexpected switch argument: {userChoice}");
        }

        Console.WriteLine();

        ConsoleUtils.HighlightConsoleLine(
            "Would you like to play again? (y/n)",
            ConsoleColor.Magenta
        );

        var playAgain = Console.ReadLine()?.Trim().ToLower();

        if (playAgain != "y")
        {
            Console.WriteLine();

            ConsoleUtils.HighlightConsoleLine("Thank you for playing! Goodbye!", ConsoleColor.Cyan);

            break;
        }

        Console.WriteLine();

        continue;
    }
}
