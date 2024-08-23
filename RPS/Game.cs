namespace RPS;

internal class Game(int requiredWins = 3, bool enableDeuce = false)
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
        outcomes[(int)RPS.Lizard, (int)RPS.Rock] = (Result.Lose, "crushes");
        outcomes[(int)RPS.Lizard, (int)RPS.Scissors] = (Result.Lose, "decapitates");

        outcomes[(int)RPS.Spock, (int)RPS.Spock] = (Result.Tie, "tie");
        outcomes[(int)RPS.Spock, (int)RPS.Scissors] = (Result.Win, "smashes");
        outcomes[(int)RPS.Spock, (int)RPS.Rock] = (Result.Win, "vaporizes");
        outcomes[(int)RPS.Spock, (int)RPS.Paper] = (Result.Lose, "disproves");
        outcomes[(int)RPS.Spock, (int)RPS.Lizard] = (Result.Lose, "poisons");
    }

    private readonly int requiredWins = requiredWins;
    private readonly int matchPointThreshold = requiredWins - 1;
    private readonly bool enableDeuce = enableDeuce;

    private int PlayerScore = 0;
    private int ComputerScore = 0;

    private int HighestScore => Math.Max(PlayerScore, ComputerScore);
    private int DiffScore => Math.Abs(PlayerScore - ComputerScore);
    private bool IsGameOver => HighestScore >= requiredWins && (!enableDeuce || DiffScore >= 2);

    public void Init()
    {
        var roundCount = 1;

        while (!IsGameOver)
        {
            Thread.Sleep(500);

            Console.WriteLine();

            ConsoleUtils.HighlightConsoleLine($"--- [ROUND {roundCount}] ---", ConsoleColor.Cyan);

            if (requiredWins > 1)
            {
                if (
                    enableDeuce
                    && PlayerScore >= matchPointThreshold
                    && ComputerScore >= matchPointThreshold
                )
                {
                    var status = DiffScore == 0 ? "Deuce" : "Advantage";

                    ConsoleUtils.HighlightConsoleLine($"[STATUS]: {status}!", ConsoleColor.Yellow);

                    Console.WriteLine();
                }
                else if (HighestScore == matchPointThreshold)
                {
                    ConsoleUtils.HighlightConsoleLine(
                        "[STATUS]: Match Point!",
                        ConsoleColor.Yellow
                    );

                    Console.WriteLine();
                }
            }

            ConsoleUtils.HighlightConsoleLine(
                "[TURN]: Pick your option from the list:",
                ConsoleColor.Magenta
            );

            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkYellow;

            foreach (var rps in rpsValues)
            {
                Console.WriteLine($"{(int)rps} - {rps}");
            }

            Console.ResetColor();

            Console.WriteLine();

            ConsoleUtils.HighlightConsoleLine("Your Choice", ConsoleColor.Magenta);

            var input = Console.ReadLine();

            Console.WriteLine();

            if (!int.TryParse(input, out int choice) || !Enum.IsDefined(typeof(RPS), choice))
            {
                ConsoleUtils.HighlightConsoleLine(
                    "[ERROR]: Invalid choice. Please enter a number corresponding to an option.",
                    ConsoleColor.Red
                );

                continue;
            }

            roundCount++;

            var playerChoice = (RPS)choice;

            var random = new Random();

            int randomIndex = random.Next(rpsValues.Length);

            var computerChoice = (RPS)rpsValues.GetValue(randomIndex)!;

            var (result, reason) = outcomes[(int)playerChoice, (int)computerChoice];

            switch (result)
            {
                case Result.Tie:
                    ConsoleUtils.HighlightConsoleLine("It's a tie!", ConsoleColor.Yellow);

                    continue;
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
            Console.WriteLine($"Your Score: {PlayerScore}");
            Console.WriteLine($"Computer Score: {ComputerScore}");
        }

        Thread.Sleep(200);

        Console.WriteLine();

        var hasPlayerWon = PlayerScore >= requiredWins && PlayerScore > ComputerScore;

        if (hasPlayerWon)
        {
            ConsoleUtils.HighlightConsoleLine("Congrats! You have won!", ConsoleColor.Green);
        }
        else
        {
            ConsoleUtils.HighlightConsoleLine("You lost! Better luck next time", ConsoleColor.Red);
        }
    }
}
