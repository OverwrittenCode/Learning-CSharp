namespace Learning;

internal abstract class GameBase(int requiredWins = 3, bool enableDeuce = false)
{
    protected readonly int requiredWins = requiredWins;
    protected readonly int matchPointThreshold = requiredWins - 1;
    protected readonly bool enableDeuce = enableDeuce;

    protected int PlayerScore = 0;
    protected int ComputerScore = 0;

    protected int HighestScore => Math.Max(PlayerScore, ComputerScore);
    protected int DiffScore => Math.Abs(PlayerScore - ComputerScore);
    protected bool IsGameOver => HighestScore >= requiredWins && (!enableDeuce || DiffScore >= 2);

    public void Init()
    {
        int roundCounter = 1;

        while (!IsGameOver)
        {
            Thread.Sleep(500);
            Console.WriteLine();

            StartRound(roundCounter++);

            if (
                requiredWins > 1
                && enableDeuce
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
                ConsoleUtils.HighlightConsoleLine("[STATUS]: Match Point!", ConsoleColor.Yellow);
                Console.WriteLine();
            }

            PlayRound();
            DisplayScores();
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
