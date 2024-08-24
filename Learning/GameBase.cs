namespace Learning;

internal abstract class GameBase(int requiredWins = 3, bool enableDeuce = false)
{
    public readonly int RequiredWins = requiredWins;
    public readonly int MatchPointThreshold = requiredWins - 1;
    public readonly bool EnableDeuce = enableDeuce;

    public int PlayerScore { get; protected set; } = 0;
    public int ComputerScore { get; protected set; } = 0;

    public int HighestScore => Math.Max(PlayerScore, ComputerScore);
    public int DiffScore => Math.Abs(PlayerScore - ComputerScore);
    public bool IsGameOver => HighestScore >= RequiredWins && (!EnableDeuce || DiffScore >= 2);

    public void Init()
    {
        int roundCounter = 1;

        while (!IsGameOver)
        {
            Thread.Sleep(500);
            Console.WriteLine();

            StartRound(roundCounter++);

            if (
                RequiredWins > 1
                && EnableDeuce
                && PlayerScore >= MatchPointThreshold
                && ComputerScore >= MatchPointThreshold
            )
            {
                var status = DiffScore == 0 ? "Deuce" : "Advantage";
                ConsoleUtils.HighlightConsoleLine($"[STATUS]: {status}!", ConsoleColor.Yellow);
                Console.WriteLine();
            }
            else if (HighestScore == MatchPointThreshold)
            {
                ConsoleUtils.HighlightConsoleLine("[STATUS]: Match Point!", ConsoleColor.Yellow);
                Console.WriteLine();
            }

            PlayRound();
            DisplayScores();
        }

        Thread.Sleep(200);
        Console.WriteLine();

        var hasPlayerWon = PlayerScore >= RequiredWins && PlayerScore > ComputerScore;

        if (hasPlayerWon)
        {
            ConsoleUtils.HighlightConsoleLine("Congrats! You have won!", ConsoleColor.Green);
        }
        else
        {
            ConsoleUtils.HighlightConsoleLine("You lost! Better luck next time", ConsoleColor.Red);
        }
    }

    protected virtual void StartRound(int roundCounter)
    {
        ConsoleUtils.HighlightConsoleLine($"--- [ROUND {roundCounter}] ---", ConsoleColor.Cyan);
    }

    protected abstract void PlayRound();

    protected void DisplayScores()
    {
        Console.WriteLine($"Your Score: {PlayerScore}");
        Console.WriteLine($"Computer Score: {ComputerScore}");
    }
}
