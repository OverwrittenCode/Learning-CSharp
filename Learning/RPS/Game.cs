namespace Learning.RPS;

internal class Game(int requiredWins = 3, bool enableDeuce = false)
    : GameBase(requiredWins, enableDeuce)
{
    private static readonly RPS[] rpsValues = Enum.GetValues<RPS>();

    private static readonly (Result result, string reason)[,] outcomes = new (
        Result result,
        string reason
    )[5, 5];

    static Game()
    {
        outcomes[(int)RPS.Rock, (int)RPS.Rock] = (Result.Tie, "tie");
        outcomes[(int)RPS.Rock, (int)RPS.Scissors] = (Result.Win, "crushes");
        outcomes[(int)RPS.Rock, (int)RPS.Lizard] = (Result.Win, "crushes");
        outcomes[(int)RPS.Rock, (int)RPS.Paper] = (Result.Lose, "covers");
        outcomes[(int)RPS.Rock, (int)RPS.Spock] = (Result.Lose, "vaporizes");

        outcomes[(int)RPS.Paper, (int)RPS.Paper] = (Result.Tie, "tie");
        outcomes[(int)RPS.Paper, (int)RPS.Rock] = (Result.Win, "covers");
        outcomes[(int)RPS.Paper, (int)RPS.Spock] = (Result.Win, "disproves");
        outcomes[(int)RPS.Paper, (int)RPS.Scissors] = (Result.Lose, "cuts");
        outcomes[(int)RPS.Paper, (int)RPS.Lizard] = (Result.Lose, "eats");

        outcomes[(int)RPS.Scissors, (int)RPS.Scissors] = (Result.Tie, "tie");
        outcomes[(int)RPS.Scissors, (int)RPS.Paper] = (Result.Win, "cuts");
        outcomes[(int)RPS.Scissors, (int)RPS.Lizard] = (Result.Win, "decapitates");
        outcomes[(int)RPS.Scissors, (int)RPS.Rock] = (Result.Lose, "crushes");
        outcomes[(int)RPS.Scissors, (int)RPS.Spock] = (Result.Lose, "smashes");

        outcomes[(int)RPS.Lizard, (int)RPS.Lizard] = (Result.Tie, "tie");
        outcomes[(int)RPS.Lizard, (int)RPS.Spock] = (Result.Win, "poisons");
        outcomes[(int)RPS.Lizard, (int)RPS.Paper] = (Result.Win, "eats");
        outcomes[(int)RPS.Lizard, (int)RPS.Scissors] = (Result.Lose, "decapitates");
        outcomes[(int)RPS.Lizard, (int)RPS.Rock] = (Result.Lose, "crushes");

        outcomes[(int)RPS.Spock, (int)RPS.Spock] = (Result.Tie, "tie");
        outcomes[(int)RPS.Spock, (int)RPS.Scissors] = (Result.Win, "smashes");
        outcomes[(int)RPS.Spock, (int)RPS.Rock] = (Result.Win, "vaporizes");
        outcomes[(int)RPS.Spock, (int)RPS.Paper] = (Result.Lose, "disproves");
        outcomes[(int)RPS.Spock, (int)RPS.Lizard] = (Result.Lose, "poisons");
    }

    protected override void PlayRound()
    {
        ConsoleUtils.HighlightConsoleLine(
            "[TURN]: Pick your option from the list:",
            ConsoleColor.Magenta
        );

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        for (int i = 0; i < rpsValues.Length; i++)
        {
            Console.WriteLine($"{i} - {rpsValues[i]}");
        }

        Console.ResetColor();
        Console.WriteLine();

        int playerChoiceInput;

        while (true)
        {
            if (
                int.TryParse(Console.ReadLine(), out playerChoiceInput)
                && Enum.IsDefined(typeof(RPS), playerChoiceInput)
            )
            {
                break;
            }

            ConsoleUtils.HighlightConsoleLine(
                "[ERROR]: Invalid choice. Please enter a number corresponding to an option.",
                ConsoleColor.Red
            );
        }

        var playerChoice = (RPS)playerChoiceInput;
        var random = new Random();
        var computerChoice = rpsValues[random.Next(rpsValues.Length)];

        var (result, reason) = outcomes[(int)playerChoice, (int)computerChoice];

        switch (result)
        {
            case Result.Tie:
                ConsoleUtils.HighlightConsoleLine("It's a tie!", ConsoleColor.Yellow);

                break;
            case Result.Win:
                PlayerScore++;

                ConsoleUtils.HighlightConsoleLine(
                    $"You win this round! {playerChoice} {reason} {computerChoice}",
                    ConsoleColor.Green
                );

                break;
            case Result.Lose:
                ComputerScore++;

                ConsoleUtils.HighlightConsoleLine(
                    $"Computer wins this round! {computerChoice} {reason} {playerChoice}",
                    ConsoleColor.Red
                );

                break;
        }

        Console.WriteLine();

        DisplayScores();
    }
}
