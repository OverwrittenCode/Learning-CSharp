using System.ComponentModel;
using Learning.Utils;

namespace Learning.Games;

internal abstract class BaseGame(int requiredWins, bool enableDeuce)
{
    public readonly int RequiredWins = requiredWins;
    public readonly int MatchPointThreshold = requiredWins - 1;
    public readonly bool EnableDeuce = enableDeuce;

    public int RoundCounter { get; private set; } = 1;
    public int PlayerScore { get; private set; } = 0;
    public int ComputerScore { get; private set; } = 0;

    public int HighestScore => Math.Max(PlayerScore, ComputerScore);
    public int DiffScore => Math.Abs(PlayerScore - ComputerScore);
    public bool IsGameOver => HighestScore >= RequiredWins && (!EnableDeuce || DiffScore >= 2);

    public void Init()
    {
        DisplayCurrentRound();

        while (!IsGameOver)
        {
            PlayTurn();
        }

        Thread.Sleep(200);
        Console.WriteLine();

        ConsoleUtils.HighlightConsoleLine("[FINAL SCORE]:", ConsoleColor.Yellow);

        DisplayCurrentScore();

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

    protected abstract void PlayTurn();

    protected virtual void PrepareNextRound() { }

    protected void EndRound(GameResult result, string? reason = "")
    {
        PrepareNextRound();

        RoundCounter++;

        string resultMessage;
        ConsoleColor colour;

        switch (result)
        {
            case GameResult.Tie:
                resultMessage = "It's a tie!";
                colour = ConsoleColor.Yellow;

                break;
            case GameResult.Win:
                PlayerScore++;

                resultMessage = "You win this round!";
                colour = ConsoleColor.Green;

                break;
            case GameResult.Lose:
                ComputerScore++;

                resultMessage = "Computer wins this round!";
                colour = ConsoleColor.Red;

                break;
            default:
                throw new InvalidEnumArgumentException($"Unexpected switch argument: {result}");
        }

        if (!string.IsNullOrEmpty(reason))
        {
            resultMessage += $" {reason}";
        }

        Console.WriteLine();
        ConsoleUtils.HighlightConsoleLine(resultMessage, colour);

        Thread.Sleep(500);

        if (IsGameOver)
        {
            return;
        }

        DisplayCurrentRound();
        DisplayCurrentScore();

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
    }

    private void DisplayCurrentRound()
    {
        ConsoleUtils.HighlightConsoleLine($"--- [ROUND {RoundCounter}] ---", ConsoleColor.Cyan);
    }

    private void DisplayCurrentScore()
    {
        Console.WriteLine($"Your Score: {PlayerScore}");
        Console.WriteLine($"Computer Score: {ComputerScore}");

        Console.WriteLine();
    }
}
