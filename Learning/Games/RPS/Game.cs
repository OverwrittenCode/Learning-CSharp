using Learning.Utils;

namespace Learning.Games.RPS;

internal class Game(int requiredWins = 3, bool enableDeuce = false)
    : BaseGame(requiredWins, enableDeuce)
{
    private static readonly RPS[] RPSValues = Enum.GetValues<RPS>();

    private static readonly (GameResult result, string action)[,] Outcomes = new (
        GameResult result,
        string action
    )[5, 5];

    static Game()
    {
        Outcomes[(int)RPS.Rock, (int)RPS.Rock] = (GameResult.Tie, "tie");
        Outcomes[(int)RPS.Rock, (int)RPS.Scissors] = (GameResult.Win, "crushes");
        Outcomes[(int)RPS.Rock, (int)RPS.Lizard] = (GameResult.Win, "crushes");
        Outcomes[(int)RPS.Rock, (int)RPS.Paper] = (GameResult.Lose, "covers");
        Outcomes[(int)RPS.Rock, (int)RPS.Spock] = (GameResult.Lose, "vaporizes");

        Outcomes[(int)RPS.Paper, (int)RPS.Paper] = (GameResult.Tie, "tie");
        Outcomes[(int)RPS.Paper, (int)RPS.Rock] = (GameResult.Win, "covers");
        Outcomes[(int)RPS.Paper, (int)RPS.Spock] = (GameResult.Win, "disproves");
        Outcomes[(int)RPS.Paper, (int)RPS.Scissors] = (GameResult.Lose, "cuts");
        Outcomes[(int)RPS.Paper, (int)RPS.Lizard] = (GameResult.Lose, "eats");

        Outcomes[(int)RPS.Scissors, (int)RPS.Scissors] = (GameResult.Tie, "tie");
        Outcomes[(int)RPS.Scissors, (int)RPS.Paper] = (GameResult.Win, "cuts");
        Outcomes[(int)RPS.Scissors, (int)RPS.Lizard] = (GameResult.Win, "decapitates");
        Outcomes[(int)RPS.Scissors, (int)RPS.Rock] = (GameResult.Lose, "crushes");
        Outcomes[(int)RPS.Scissors, (int)RPS.Spock] = (GameResult.Lose, "smashes");

        Outcomes[(int)RPS.Lizard, (int)RPS.Lizard] = (GameResult.Tie, "tie");
        Outcomes[(int)RPS.Lizard, (int)RPS.Spock] = (GameResult.Win, "poisons");
        Outcomes[(int)RPS.Lizard, (int)RPS.Paper] = (GameResult.Win, "eats");
        Outcomes[(int)RPS.Lizard, (int)RPS.Scissors] = (GameResult.Lose, "decapitates");
        Outcomes[(int)RPS.Lizard, (int)RPS.Rock] = (GameResult.Lose, "crushes");

        Outcomes[(int)RPS.Spock, (int)RPS.Spock] = (GameResult.Tie, "tie");
        Outcomes[(int)RPS.Spock, (int)RPS.Scissors] = (GameResult.Win, "smashes");
        Outcomes[(int)RPS.Spock, (int)RPS.Rock] = (GameResult.Win, "vaporizes");
        Outcomes[(int)RPS.Spock, (int)RPS.Paper] = (GameResult.Lose, "disproves");
        Outcomes[(int)RPS.Spock, (int)RPS.Lizard] = (GameResult.Lose, "poisons");
    }

    protected override void PlayTurn()
    {
        var playerChoice = ConsoleUtils.GetEnumChoice(RPSValues);

        var random = new Random();
        var computerChoice = RPSValues[random.Next(RPSValues.Length)];

        var (result, action) = Outcomes[(int)playerChoice, (int)computerChoice];

        var reason = result switch
        {
            GameResult.Win => $"{playerChoice} {action} {computerChoice}",
            GameResult.Lose => $"{computerChoice} {action} {playerChoice}",
            _ => null,
        };

        EndRound(result, reason);
    }
}
