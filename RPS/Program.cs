using RPS;

ConsoleUtils.HighlightConsoleLine("----- [RPS GAME] -----", ConsoleColor.Cyan);

while (true)
{
    ConsoleUtils.HighlightConsoleLine(
        "[CONFIG]: How many wins are required to end the game?",
        ConsoleColor.Magenta
    );

    var input = Console.ReadLine();

    if (int.TryParse(input, out int requiredWins) && requiredWins > 0)
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

        var game = new Game(requiredWins, enableDeuce);

        game.Init();

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

    ConsoleUtils.HighlightConsoleLine(
        "[ERROR]: Please enter a positive integer.",
        ConsoleColor.Red
    );
}
