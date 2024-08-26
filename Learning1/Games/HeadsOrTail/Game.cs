using Learning1.Utils;

namespace Learning1.Games.HeadsOrTail;

internal class Game(int requiredWins = 3, bool enableDeuce = false)
    : BaseGame(requiredWins, enableDeuce)
{
    private static readonly CoinFlip[] CoinFlips = Enum.GetValues<CoinFlip>();

    protected override void PlayTurn()
    {
        CoinFlip playerChoice = ConsoleUtils.GetEnumChoice(CoinFlips);
        CoinFlip coinFlip = ObjectUtils.GetRandomElement(CoinFlips);

        if (playerChoice == coinFlip)
        {
            EndRound(RoundOutcome.Win, "Your guess was correct!");

            return;
        }

        EndRound(RoundOutcome.Lose, $"It landed on {coinFlip}, better luck next time!");
    }
}
