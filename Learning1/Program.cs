using System.ComponentModel;
using Learning1.Games;
using Learning1.Utils;

const int MinRequiredWins = 0;
const int MaxRequiredWins = 8;

var AllowedRangeNotice = $"({MinRequiredWins} - {MaxRequiredWins})";

var userChoice = ConsoleUtils.GetEnumChoice<GameType>();

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

    Console.WriteLine();

    int requiredWins;

    while (
        !Int32.TryParse(Console.ReadLine(), out requiredWins)
        || requiredWins <= MinRequiredWins
        || requiredWins >= MaxRequiredWins
    )
    {
        ConsoleUtils.HighlightConsoleLine(
            $"[ERROR]: Please enter a valid integer {AllowedRangeNotice}",
            ConsoleColor.Red
        );
    }

    var enableDeuce = false;

    if (requiredWins > 2)
    {
        Console.WriteLine();

        ConsoleUtils.HighlightConsoleLine(
            "[CONFIG]: Enable deuce mode? (y/n)",
            ConsoleColor.Magenta
        );

        enableDeuce = Console.ReadLine()?.Trim().ToLower() == "y";
    }

    switch (userChoice)
    {
        case GameType.Rps:
            new Learning1.Games.RPS.Game(requiredWins, enableDeuce).Init();

            break;
        case GameType.TicTacToe:
            new Learning1.Games.TicTacToe.Game(requiredWins, enableDeuce).Init();

            break;
        case GameType.HeadsOrTails:
            new Learning1.Games.HeadsOrTail.Game(requiredWins, enableDeuce).Init();

            break;
        default:
            throw new InvalidEnumArgumentException($"Unexpected switch argument: {userChoice}");
    }

    Console.WriteLine();

    ConsoleUtils.HighlightConsoleLine("Would you like to play again? (y/n)", ConsoleColor.Magenta);

    var isContinuePlaying = Console.ReadLine()?.Trim().ToLower() == "y";

    Console.WriteLine();

    if (isContinuePlaying)
    {
        continue;
    }

    ConsoleUtils.HighlightConsoleLine("Thank you for playing! Goodbye!", ConsoleColor.Cyan);

    break;
}
