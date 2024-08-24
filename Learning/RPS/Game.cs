namespace Learning.RPS;

internal class Game(int requiredWins = 3, bool enableDeuce = false)
    : GameBase(requiredWins, enableDeuce)
{
    private static readonly RPS[] RPSValues = Enum.GetValues<RPS>();

    private static readonly (Result result, string action)[,] Outcomes = new (
        Result result,
        string action
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

    protected override void PlayTurn()
    {
        var playerChoice = ConsoleUtils.GetEnumChoice(RPSValues);

        var random = new Random();
        var computerChoice = RPSValues[random.Next(RPSValues.Length)];

        var (result, action) = Outcomes[(int)playerChoice, (int)computerChoice];

        var reason = result switch
        {
            Result.Win => $"{playerChoice} {action} {computerChoice}",
            Result.Lose => $"{computerChoice} {action} {playerChoice}",
            _ => null,
        };

        EndRound(result, reason);
    }
}
