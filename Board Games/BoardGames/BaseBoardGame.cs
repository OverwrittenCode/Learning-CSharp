using System.ComponentModel;
using Common.Utils;

namespace BoardGames;

public abstract class BaseBoardGame
{
    private const string MessageCategory = "Turn";

    protected static T GetPlayerChoice<T>(T[] values) where T : struct, Enum => ConsoleUtils.GetEnumChoice(values, MessageCategory);
    protected static T GetPlayerChoice<T>() where T : struct, Enum => ConsoleUtils.GetEnumChoice<T>(MessageCategory);

    private byte RequiredWins { get; }
    private int MatchPointThreshold { get; }
    private bool EnableDeuce { get; }

    private int HighestScore => Math.Max(PlayerScore, ComputerScore);
    private int DiffScore => Math.Abs(PlayerScore - ComputerScore);
    private bool IsGameOver => HighestScore >= RequiredWins && (!EnableDeuce || DiffScore >= 2);

    private int RoundCounter { get; set; }
    private int PlayerScore { get; set; }
    private int ComputerScore { get; set; }

    protected BaseBoardGame()
    {
        var allowedRangeNotice = $"from 1 to {Byte.MaxValue}";

        ConsoleUtils.HighlightConsoleLine($"[CONFIG]: How many wins are required to end the game ({allowedRangeNotice})?", ConsoleColor.Magenta);

        byte requiredWins;

        while (!Byte.TryParse(Console.ReadLine(), out requiredWins) || requiredWins is 0)
        {
            ConsoleUtils.HighlightConsoleLine($"[ERROR]: Please enter a valid win amount ({allowedRangeNotice})", ConsoleColor.Red);
        }

        var enableDeuce = false;

        if (requiredWins > 2)
        {
            enableDeuce = ConsoleUtils.GetBooleanChoice("Enable deuce mode");
        }

        RequiredWins = requiredWins;
        MatchPointThreshold = requiredWins - 1;
        EnableDeuce = enableDeuce;
    }

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

    protected void EndRound(RoundOutcome result, string? reason = "")
    {
        PrepareNextRound();

        RoundCounter++;

        string resultMessage;
        ConsoleColor colour;

        switch (result)
        {
            case RoundOutcome.Tie:
                resultMessage = "It's a tie!";
                colour = ConsoleColor.Yellow;

                break;
            case RoundOutcome.Win:
                PlayerScore++;

                resultMessage = "You win this round!";
                colour = ConsoleColor.Green;

                break;
            case RoundOutcome.Lose:
                ComputerScore++;

                resultMessage = "Computer wins this round!";
                colour = ConsoleColor.Red;

                break;
            default:
                throw new InvalidEnumArgumentException($"Unexpected switch argument: {result}");
        }

        if (!String.IsNullOrEmpty(reason))
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

        if (RequiredWins > 1 && EnableDeuce && PlayerScore >= MatchPointThreshold && ComputerScore >= MatchPointThreshold)
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

    protected abstract void PlayTurn();

    protected virtual void PrepareNextRound() { }

    private void DisplayCurrentRound() => ConsoleUtils.HighlightConsoleLine($"--- [ROUND {RoundCounter}] ---", ConsoleColor.Cyan);

    private void DisplayCurrentScore()
    {
        Console.WriteLine($"Your Score: {PlayerScore}");
        Console.WriteLine($"Computer Score: {ComputerScore}");

        Console.WriteLine();
    }
}
