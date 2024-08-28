using Common.Extensions;

namespace Learning1.Games.RPS;

internal class Game(int requiredWins = 3, bool enableDeuce = false)
    : BaseGame(requiredWins, enableDeuce)
{
    private static readonly RPS[] RPSValues = Enum.GetValues<RPS>();

    private static readonly (RoundOutcome result, string action)[,] Outcomes = new (
        RoundOutcome result,
        string action
    )[5, 5];

    static Game()
    {
        Outcomes[(int)RPS.Rock, (int)RPS.Rock] = (RoundOutcome.Tie, "tie");
        Outcomes[(int)RPS.Rock, (int)RPS.Scissors] = (RoundOutcome.Win, "crushes");
        Outcomes[(int)RPS.Rock, (int)RPS.Lizard] = (RoundOutcome.Win, "crushes");
        Outcomes[(int)RPS.Rock, (int)RPS.Paper] = (RoundOutcome.Lose, "covers");
        Outcomes[(int)RPS.Rock, (int)RPS.Spock] = (RoundOutcome.Lose, "vaporizes");

        Outcomes[(int)RPS.Paper, (int)RPS.Paper] = (RoundOutcome.Tie, "tie");
        Outcomes[(int)RPS.Paper, (int)RPS.Rock] = (RoundOutcome.Win, "covers");
        Outcomes[(int)RPS.Paper, (int)RPS.Spock] = (RoundOutcome.Win, "disproves");
        Outcomes[(int)RPS.Paper, (int)RPS.Scissors] = (RoundOutcome.Lose, "cuts");
        Outcomes[(int)RPS.Paper, (int)RPS.Lizard] = (RoundOutcome.Lose, "eats");

        Outcomes[(int)RPS.Scissors, (int)RPS.Scissors] = (RoundOutcome.Tie, "tie");
        Outcomes[(int)RPS.Scissors, (int)RPS.Paper] = (RoundOutcome.Win, "cuts");
        Outcomes[(int)RPS.Scissors, (int)RPS.Lizard] = (RoundOutcome.Win, "decapitates");
        Outcomes[(int)RPS.Scissors, (int)RPS.Rock] = (RoundOutcome.Lose, "crushes");
        Outcomes[(int)RPS.Scissors, (int)RPS.Spock] = (RoundOutcome.Lose, "smashes");

        Outcomes[(int)RPS.Lizard, (int)RPS.Lizard] = (RoundOutcome.Tie, "tie");
        Outcomes[(int)RPS.Lizard, (int)RPS.Spock] = (RoundOutcome.Win, "poisons");
        Outcomes[(int)RPS.Lizard, (int)RPS.Paper] = (RoundOutcome.Win, "eats");
        Outcomes[(int)RPS.Lizard, (int)RPS.Scissors] = (RoundOutcome.Lose, "decapitates");
        Outcomes[(int)RPS.Lizard, (int)RPS.Rock] = (RoundOutcome.Lose, "crushes");

        Outcomes[(int)RPS.Spock, (int)RPS.Spock] = (RoundOutcome.Tie, "tie");
        Outcomes[(int)RPS.Spock, (int)RPS.Scissors] = (RoundOutcome.Win, "smashes");
        Outcomes[(int)RPS.Spock, (int)RPS.Rock] = (RoundOutcome.Win, "vaporizes");
        Outcomes[(int)RPS.Spock, (int)RPS.Paper] = (RoundOutcome.Lose, "disproves");
        Outcomes[(int)RPS.Spock, (int)RPS.Lizard] = (RoundOutcome.Lose, "poisons");
    }

    protected override void PlayTurn()
    {
        RPS playerChoice = GetPlayerChoice(RPSValues);
        RPS computerChoice = RPSValues.GetRandomElement();

        var (result, action) = Outcomes[(int)playerChoice, (int)computerChoice];

        var reason = result switch
        {
            RoundOutcome.Win => $"{playerChoice} {action} {computerChoice}",
            RoundOutcome.Lose => $"{computerChoice} {action} {playerChoice}",
            _ => null,
        };

        EndRound(result, reason);
    }
}
