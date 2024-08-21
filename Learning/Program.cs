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

    public class Game(int requiredWins = 3, bool enableDeuce = false)
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

        private readonly int requiredWins = requiredWins;
        private readonly bool enableDeuce = enableDeuce;

        public (int Player, int Computer) Scores = (Player: 0, Computer: 0);

        private int HighestScore => Math.Max(Scores.Computer, Scores.Player);
        private int DiffScore => Math.Abs(Scores.Computer - Scores.Player);
        private bool IsGameOver => HighestScore >= requiredWins && (!enableDeuce || requiredWins == 1 || DiffScore >= 2);

        public async Task Init()
        {
            var roundCount = 1;

            while (!IsGameOver)
            {
                await Task.Delay(500);

                Console.WriteLine();

                HighlightConsoleLine(str: $"--- [ROUND {roundCount}] ---", colour: ConsoleColor.Cyan);

                if (enableDeuce)
                {
                    if (DiffScore == 0 && HighestScore == requiredWins - 1)
                    {
                        HighlightConsoleLine(str: "[STATUS]: Deuce!", colour: ConsoleColor.Yellow);

                        Console.WriteLine();
                    }
                    else if (DiffScore == 1 && HighestScore >= requiredWins)
                    {
                        HighlightConsoleLine(str: "[STATUS]: Advantage! Match Point!", colour: ConsoleColor.Yellow);

                        Console.WriteLine();
                    }
                }
                else if (requiredWins > 1 && HighestScore == requiredWins - 1)
                {
                    HighlightConsoleLine(str: "[STATUS]: Match Point!", colour: ConsoleColor.Yellow);

                    Console.WriteLine();
                }

                HighlightConsoleLine(str: "[TURN]: Pick your option from the list:", colour: ConsoleColor.Magenta);

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.DarkGreen;

                foreach (var rps in rpsOptions)
                {
                    Console.WriteLine($"{(int) rps} - {rps}");
                }

                Console.ResetColor();

                Console.WriteLine();

                HighlightConsoleLine(str: "Your Choice", colour: ConsoleColor.Magenta);

                var input = Console.ReadLine();

                Console.WriteLine();

                if (!int.TryParse(input, out int choice) || !Enum.IsDefined(typeof(RPS), choice))
                {
                    HighlightConsoleLine(
                        str: "[ERROR]: Invalid choice. Please enter a number corresponding to an option.",
                        colour: ConsoleColor.Red
                    );

                    continue;
                }

                roundCount++;

                var playerChoice = (RPS) choice;

                var random = new Random();

                var randomIndex = random.Next(rpsOptions.Length);

                var computerChoice = (RPS) rpsOptions.GetValue(randomIndex)!;

                if (playerChoice == computerChoice)
                {
                    HighlightConsoleLine(str: "It's a tie!", colour: ConsoleColor.Yellow);

                    continue;
                }

                var isPlayerRoundWinner = outcomes[playerChoice]
                    .Exists(o => o.defeats == computerChoice);

                if (isPlayerRoundWinner)
                {
                    Scores.Player++;

                    var (defeats, reason) = outcomes[playerChoice]
                        .Find(o => o.defeats == computerChoice);

                    HighlightConsoleLine(
                       str: $"You win this round! {playerChoice} {reason} {computerChoice}",
                       colour: ConsoleColor.Green
                    );
                }
                else
                {
                    Scores.Computer++;

                    var (defeats, reason) = outcomes[computerChoice]
                        .Find(o => o.defeats == playerChoice);

                    HighlightConsoleLine(
                       str: $"Computer wins this round! {computerChoice} {reason} {playerChoice}",
                       colour: ConsoleColor.Red
                    );
                }

                Console.WriteLine();
                Console.WriteLine($"Your Score: {Scores.Player}");
                Console.WriteLine($"Computer Score: {Scores.Computer}");
            }

            await Task.Delay(200);

            Console.WriteLine();

            var hasPlayerWon = Scores.Player >= requiredWins && Scores.Player > Scores.Computer;

            if (hasPlayerWon)
            {
                HighlightConsoleLine(str: "Congrats! You have won!", colour: ConsoleColor.Green);
            }
            else
            {
                HighlightConsoleLine(str: "You lost! Better luck next time", colour: ConsoleColor.Red);
            }
        }

        public static async Task Main()
        {
            HighlightConsoleLine(str: "----- [RPS GAME] -----", colour: ConsoleColor.Cyan);

            while (true)
            {
                HighlightConsoleLine(str: "[CONFIG]: How many wins are required to end the game?", colour: ConsoleColor.Magenta);

                var input = Console.ReadLine();

                if (int.TryParse(input, out int requiredWins) && requiredWins > 0)
                {
                    Console.WriteLine();

                    HighlightConsoleLine(str: "[CONFIG]: Enable deuce mode? (y/n)", colour: ConsoleColor.Magenta);

                    var deuceInput = Console.ReadLine();

                    var enableDeuce = deuceInput?.Trim().ToLower() == "y";

                    var game = new Game(requiredWins, enableDeuce);

                    await game.Init();

                    Console.WriteLine();

                    HighlightConsoleLine("Would you like to play again? (y/n)", ConsoleColor.Magenta);

                    var playAgain = Console.ReadLine()?.Trim().ToLower();

                    if (playAgain != "y")
                    {
                        Console.WriteLine();

                        HighlightConsoleLine("Thank you for playing! Goodbye!", ConsoleColor.Cyan);

                        break;
                    }

                    Console.WriteLine();

                    continue;
                }

                HighlightConsoleLine(str: "[ERROR]: Please enter a positive integer.", colour: ConsoleColor.Red);
            }

        }

        private static void HighlightConsoleLine(string str, ConsoleColor colour)
        {
            var originalColour = Console.ForegroundColor;

            Console.ForegroundColor = colour;

            Console.WriteLine(str);

            Console.ForegroundColor = originalColour;
        }
    }
}
