namespace EliteAthletics;

internal enum Gender
{
    Male,
    Female
}

internal enum RecordBreaker
{
    World,
    European,
    British
}

internal static class Program
{
    private static readonly Dictionary<Gender, Dictionary<RecordBreaker, double>> Records = new()
    {
        {
            Gender.Male, new()
            {
                {
                    RecordBreaker.World, 9.58
                },
                {
                    RecordBreaker.European, 9.86
                },
                {
                    RecordBreaker.British, 9.87
                }
            }
        },
        {
            Gender.Female, new()
            {
                {
                    RecordBreaker.World, 10.49
                },
                {
                    RecordBreaker.European, 10.73
                },
                {
                    RecordBreaker.British, 10.99
                }
            }
        }
    };

    private static readonly string GenderOptions = String.Join(", ", Enum.GetValues<Gender>());

    private static void Main()
    {
        Dictionary<RecordBreaker, double> focusedRecords;
        while (true)
        {
            Console.WriteLine(GenderOptions);
            Console.Write("Enter gender group: ");

            var input = Console.ReadLine()?.Trim();
            Console.WriteLine();

            if (Enum.TryParse(input, true, out Gender genderGroup) && Enum.IsDefined(genderGroup))
            {
                focusedRecords = Records[genderGroup];
                break;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please try again.");
            Console.ResetColor();
        }

        int numberOfAthletes;
        while (true)
        {
            const int MinAthletes = 4;
            const int MaxAthletes = 8;

            Console.Write($"Enter number of athletes (from {MinAthletes} to {MaxAthletes}): ");
            var input = Console.ReadLine();
            Console.WriteLine();

            if (Int32.TryParse(input, out numberOfAthletes) && numberOfAthletes is >= MinAthletes and <= MaxAthletes)
            {
                break;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please try again.");
            Console.ResetColor();
        }

        for (var i = 0; i < numberOfAthletes; i++)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[Athlete {i + 1}/{numberOfAthletes}]");
            Console.ResetColor();

            double timeTaken;
            while (true)
            {
                Console.Write("Enter time taken for 100m race in seconds: ");
                var input = Console.ReadLine();
                Console.WriteLine();

                if (Double.TryParse(input, out timeTaken) && timeTaken > 0)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ResetColor();
            }

            foreach (KeyValuePair<RecordBreaker, double> pair in focusedRecords.Where(pair => timeTaken < pair.Value))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(
                    $"""
                     [{pair.Key.ToString().ToUpper()} RECORD BEATEN]
                     {"Old Record:",-15} {pair.Value:N2}s
                     {"New Record:",-15} {timeTaken:N2}s

                     """
                );
                Console.ResetColor();

                break;
            }
        }
    }
}
