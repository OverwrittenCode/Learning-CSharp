using Common.Utils;

const int MinRequiredWins = 0;
const int MaxRequiredWins = 8;

var allowedRangeNotice = $"({MinRequiredWins} - {MaxRequiredWins})";

Games.GameType userChoice = ConsoleUtils.GetEnumChoice<Games.GameType>();

ConsoleUtils.HighlightConsoleLine($"----- [{userChoice}] -----", ConsoleColor.Cyan);

while (true)
{
    ConsoleUtils.HighlightConsoleLine(
        $"[CONFIG]: How many wins are required to end the game {allowedRangeNotice}?",
        ConsoleColor.Magenta
    );

    int requiredWins;

    while (
        !Int32.TryParse(Console.ReadLine(), out requiredWins)
        || requiredWins <= MinRequiredWins
        || requiredWins >= MaxRequiredWins
    )
    {
        ConsoleUtils.HighlightConsoleLine(
            $"[ERROR]: Please enter a valid integer {allowedRangeNotice}",
            ConsoleColor.Red
        );
    }

    var enableDeuce = false;

    if (requiredWins > 2)
    {
        Console.WriteLine();

        enableDeuce = ConsoleUtils.GetBooleanChoice("Enable deuce mode");
    }

    switch (userChoice)
    {
        case Games.GameType.Rps:
            new Games.RPS.Game(requiredWins, enableDeuce).Init();

            break;
        case Games.GameType.TicTacToe:
            new Games.TicTacToe.Game(requiredWins, enableDeuce).Init();

            break;
        case Games.GameType.HeadsOrTails:
            new Games.HeadsOrTail.Game(requiredWins, enableDeuce).Init();

            break;
    }

    Console.WriteLine();

    var isContinuePlaying = ConsoleUtils.GetBooleanChoice("Would you like to play again");

    Console.WriteLine();

    if (isContinuePlaying)
    {
        continue;
    }

    ConsoleUtils.HighlightConsoleLine("Thank you for playing! Goodbye!", ConsoleColor.Cyan);

    break;
}
