using Common.Extensions;

namespace BoardGames.Rps;

internal sealed class Game : BaseBoardGame
{
    private static readonly Move[] Moves = Enum.GetValues<Move>();

    private static readonly (RoundOutcome result, string action)[,] Outcomes = new ( RoundOutcome result, string action )[5, 5];

    static Game()
    {
        Outcomes[(int)Move.Rock, (int)Move.Rock] = (RoundOutcome.Tie, "tie");
        Outcomes[(int)Move.Rock, (int)Move.Scissors] = (RoundOutcome.Win, "crushes");
        Outcomes[(int)Move.Rock, (int)Move.Lizard] = (RoundOutcome.Win, "crushes");
        Outcomes[(int)Move.Rock, (int)Move.Paper] = (RoundOutcome.Lose, "covers");
        Outcomes[(int)Move.Rock, (int)Move.Spock] = (RoundOutcome.Lose, "vaporizes");

        Outcomes[(int)Move.Paper, (int)Move.Paper] = (RoundOutcome.Tie, "tie");
        Outcomes[(int)Move.Paper, (int)Move.Rock] = (RoundOutcome.Win, "covers");
        Outcomes[(int)Move.Paper, (int)Move.Spock] = (RoundOutcome.Win, "disproves");
        Outcomes[(int)Move.Paper, (int)Move.Scissors] = (RoundOutcome.Lose, "cuts");
        Outcomes[(int)Move.Paper, (int)Move.Lizard] = (RoundOutcome.Lose, "eats");

        Outcomes[(int)Move.Scissors, (int)Move.Scissors] = (RoundOutcome.Tie, "tie");
        Outcomes[(int)Move.Scissors, (int)Move.Paper] = (RoundOutcome.Win, "cuts");
        Outcomes[(int)Move.Scissors, (int)Move.Lizard] = (RoundOutcome.Win, "decapitates");
        Outcomes[(int)Move.Scissors, (int)Move.Rock] = (RoundOutcome.Lose, "crushes");
        Outcomes[(int)Move.Scissors, (int)Move.Spock] = (RoundOutcome.Lose, "smashes");

        Outcomes[(int)Move.Lizard, (int)Move.Lizard] = (RoundOutcome.Tie, "tie");
        Outcomes[(int)Move.Lizard, (int)Move.Spock] = (RoundOutcome.Win, "poisons");
        Outcomes[(int)Move.Lizard, (int)Move.Paper] = (RoundOutcome.Win, "eats");
        Outcomes[(int)Move.Lizard, (int)Move.Scissors] = (RoundOutcome.Lose, "decapitates");
        Outcomes[(int)Move.Lizard, (int)Move.Rock] = (RoundOutcome.Lose, "crushes");

        Outcomes[(int)Move.Spock, (int)Move.Spock] = (RoundOutcome.Tie, "tie");
        Outcomes[(int)Move.Spock, (int)Move.Scissors] = (RoundOutcome.Win, "smashes");
        Outcomes[(int)Move.Spock, (int)Move.Rock] = (RoundOutcome.Win, "vaporizes");
        Outcomes[(int)Move.Spock, (int)Move.Paper] = (RoundOutcome.Lose, "disproves");
        Outcomes[(int)Move.Spock, (int)Move.Lizard] = (RoundOutcome.Lose, "poisons");
    }

    protected override void PlayTurn()
    {
        Move playerChoice = GetPlayerChoice(Moves);
        Move computerChoice = Moves.GetRandomElement();

        var (result, action) = Outcomes[(int)playerChoice, (int)computerChoice];

        var reason = result switch
        {
            RoundOutcome.Win => $"{playerChoice} {action} {computerChoice}",
            RoundOutcome.Lose => $"{computerChoice} {action} {playerChoice}",
            _ => null
        };

        EndRound(result, reason);
    }
}
