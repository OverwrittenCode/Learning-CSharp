namespace Learning.RPS;

internal class Game(int requiredWins = 3, bool enableDeuce = false)
    : GameBase(requiredWins, enableDeuce)
{
    private static readonly RPS[] RPSValues = Enum.GetValues<RPS>();

    private static readonly (Result result, string reason)[,] Outcomes = new (
        Result result,
        string reason
    )[5, 5];

    static Game()
    {
        Outcomes[(int)RPS.Rock, (int)RPS.Rock] = (Result.Tie, "tie");
        Outcomes[(int)RPS.Rock, (int)RPS.Scissors] = (Result.Win, "crushes");
        Outcomes[(int)RPS.Rock, (int)RPS.Lizard] = (Result.Win, "crushes");
        Outcomes[(int)RPS.Rock, (int)RPS.Paper] = (Result.Lose, "covers");
        Outcomes[(int)RPS.Rock, (int)RPS.Spock] = (Result.Lose, "vaporizes");

        Outcomes[(int)RPS.Paper, (int)RPS.Paper] = (Result.Tie, "tie");
        Outcomes[(int)RPS.Paper, (int)RPS.Rock] = (Result.Win, "covers");
        Outcomes[(int)RPS.Paper, (int)RPS.Spock] = (Result.Win, "disproves");
        Outcomes[(int)RPS.Paper, (int)RPS.Scissors] = (Result.Lose, "cuts");
        Outcomes[(int)RPS.Paper, (int)RPS.Lizard] = (Result.Lose, "eats");

        Outcomes[(int)RPS.Scissors, (int)RPS.Scissors] = (Result.Tie, "tie");
        Outcomes[(int)RPS.Scissors, (int)RPS.Paper] = (Result.Win, "cuts");
        Outcomes[(int)RPS.Scissors, (int)RPS.Lizard] = (Result.Win, "decapitates");
        Outcomes[(int)RPS.Scissors, (int)RPS.Rock] = (Result.Lose, "crushes");
        Outcomes[(int)RPS.Scissors, (int)RPS.Spock] = (Result.Lose, "smashes");

        Outcomes[(int)RPS.Lizard, (int)RPS.Lizard] = (Result.Tie, "tie");
        Outcomes[(int)RPS.Lizard, (int)RPS.Spock] = (Result.Win, "poisons");
        Outcomes[(int)RPS.Lizard, (int)RPS.Paper] = (Result.Win, "eats");
        Outcomes[(int)RPS.Lizard, (int)RPS.Scissors] = (Result.Lose, "decapitates");
        Outcomes[(int)RPS.Lizard, (int)RPS.Rock] = (Result.Lose, "crushes");

        Outcomes[(int)RPS.Spock, (int)RPS.Spock] = (Result.Tie, "tie");
        Outcomes[(int)RPS.Spock, (int)RPS.Scissors] = (Result.Win, "smashes");
        Outcomes[(int)RPS.Spock, (int)RPS.Rock] = (Result.Win, "vaporizes");
        Outcomes[(int)RPS.Spock, (int)RPS.Paper] = (Result.Lose, "disproves");
        Outcomes[(int)RPS.Spock, (int)RPS.Lizard] = (Result.Lose, "poisons");
    }

    protected override void PlayRound()
    {
        ConsoleUtils.HighlightConsoleLine(
            "[TURN]: Pick your option from the list:",
            ConsoleColor.Magenta
        );

        Console.WriteLine();

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        for (int i = 0; i < RPSValues.Length; i++)
        {
            Console.WriteLine($"{i} - {RPSValues[i]}");
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
        var computerChoice = RPSValues[random.Next(RPSValues.Length)];

        var (result, reason) = Outcomes[(int)playerChoice, (int)computerChoice];

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
