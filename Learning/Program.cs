namespace _
{
    public enum RPS
    {
        Rock,
        Paper,
        Scissors,
        Lizard,
        Spock,
    }

    public class Game(int winThreshold = 3, bool enableDeuece = false)
    {
        private static readonly RPS[] rpsOptions = Enum.GetValues<RPS>();

        private static readonly Dictionary<RPS, List<(RPS defeats, string reason)>> outcomes =
            new()
            {
                {
                    RPS.Rock,
                    new()
                    {
                        (defeats: RPS.Scissors, reason: "crushes"),
                        (defeats: RPS.Lizard, reason: "crushes"),
                    }
                },
                {
                    RPS.Paper,
                    new()
                    {
                        (defeats: RPS.Rock, reason: "covers"),
                        (defeats: RPS.Spock, reason: "disproves"),
                    }
                },
                {
                    RPS.Scissors,
                    new()
                    {
                        (defeats: RPS.Paper, reason: "cuts"),
                        (defeats: RPS.Lizard, reason: "decapitates"),
                    }
                },
                {
                    RPS.Lizard,
                    new()
                    {
                        (defeats: RPS.Spock, reason: "poisons"),
                        (defeats: RPS.Paper, reason: "eats"),
                    }
                },
                {
                    RPS.Spock,
                    new()
                    {
                        (defeats: RPS.Scissors, reason: "smashes"),
                        (defeats: RPS.Rock, reason: "vaporizes"),
                    }
                },
            };

        private readonly int winThreshold = winThreshold;
        private readonly bool enableDeuece = enableDeuece;

        public (int Player, int Computer) Scores = (Player: 0, Computer: 0);

        private int HighestScore => Math.Max(Scores.Computer, Scores.Player);
        private int DiffScore => Math.Abs(Scores.Computer - Scores.Player);
        private bool IsGameOver =>
            HighestScore >= winThreshold && (!enableDeuece || DiffScore >= 2);

        public async Task Init()
        {
            var roundCount = 1;

            while (!IsGameOver)
            {
                await Task.Delay(200);

                var roundNotice = $"\n--- [ROUND {roundCount}] ---";

                Console.WriteLine(roundNotice);

                if (winThreshold > 1 && HighestScore > 0)
                {
                    if (HighestScore == winThreshold - 1)
                    {
                        Console.WriteLine("[STATUS]: Match Point!");
                    }

                    if (enableDeuece)
                    {
                        if (Scores.Player == Scores.Computer)
                        {
                            Console.WriteLine("[MODE]: Deuce!");
                        }
                        else if (HighestScore >= winThreshold && DiffScore == 1)
                        {
                            Console.WriteLine("[MODE]: Advantage!");
                        }
                    }
                }

                Console.WriteLine("[TURN]: Pick your option from the list:\n");

                foreach (var rps in rpsOptions)
                {
                    Console.WriteLine($"{(int) rps} - {rps}");
                }

                Console.Write("\nYour Choice\n> ");

                var input = Console.ReadLine();

                Console.WriteLine();

                if (!int.TryParse(input, out int choice) || !Enum.IsDefined(typeof(RPS), choice))
                {
                    Console.WriteLine(
                        "[ERROR]: Please enter a number corresponding to one of the options."
                    );

                    continue;
                }

                var playerChoice = (RPS) choice;

                var random = new Random();

                var randomIndex = random.Next(rpsOptions.Length);

                var computerChoice = (RPS) rpsOptions.GetValue(randomIndex)!;

                if (playerChoice == computerChoice)
                {
                    Console.WriteLine("It's a tie!");

                    continue;
                }

                var isPlayerRoundWinner = outcomes[playerChoice]
                    .Exists(o => o.defeats == computerChoice);

                if (isPlayerRoundWinner)
                {
                    Scores.Player++;

                    var (defeats, reason) = outcomes[playerChoice]
                        .Find(o => o.defeats == computerChoice);

                    Console.WriteLine(
                        $"You win this round! {playerChoice} {reason} {computerChoice}"
                    );
                }
                else
                {
                    Scores.Computer++;

                    var (defeats, reason) = outcomes[computerChoice]
                        .Find(o => o.defeats == playerChoice);

                    Console.WriteLine(
                        $"Computer wins this round! {computerChoice} {reason} {playerChoice}"
                    );
                }

                Console.WriteLine($"\n[SCORE]: {Scores.Computer} - {Scores.Player}");
            }

            await Task.Delay(200);

            var hasPlayerWon = Scores.Player >= winThreshold && Scores.Player > Scores.Computer;

            var endMessage = hasPlayerWon
                ? "Congrats! You have won!"
                : "You lost! Better luck next time.";

            Console.WriteLine($"\n {endMessage}");
        }

        public static async Task Main()
        {
            Console.WriteLine("----- [RPS GAME] -----");

            while (true)
            {
                Console.Write("[CONFIG]: How many wins are required to end the game?\n> ");

                var input = Console.ReadLine();

                if (int.TryParse(input, out int requiredWins) && requiredWins > 0)
                {
                    Console.Write("[CONFIG]: Enable deuce mode? (y/n)\n> ");

                    var deuceInput = Console.ReadLine();

                    var isDeuce = deuceInput?.Trim().ToLower() == "y";

                    var game = new Game(requiredWins, isDeuce);

                    await game.Init();

                    break;
                }

                Console.WriteLine("[ERROR]: Please enter a positive integer.");
            }
        }
    }
}
