internal enum Gender
{
    Male,
    Female,
}

internal sealed class Program
{
    private const int IdealBMI = 22;

    private const int PadMaxWidth = 65;
    private const int PadRightWidth = 45;
    private const int PadLeftWidth = PadMaxWidth - PadRightWidth - 1;

    public static double Weight { get; private set; }
    public static double Height { get; private set; }
    public static int Age { get; private set; }
    public static int DailyExerciseSessions { get; private set; }
    public static Gender Gender { get; private set; }

    public static double BMI => Math.Round(Weight / HeightInSquaredMeters, 1);
    public static string BMICategory =>
        BMI switch
        {
            < 18.5 => "Underweight",
            < 25 => "Normal",
            < 30 => "Overweight",
            _ => "Obese",
        };
    public static double WeightDifference =>
        Math.Abs(Math.Round(Weight - (IdealBMI * HeightInSquaredMeters), 2));
    public static double BMR =>
        Math.Round(
            Gender == Gender.Male
                ? 88.362 + (13.397 * Weight) + (4.799 * Height) - (5.677 * Age)
                : 447.593 + (9.247 * Weight) + (3.098 * Height) - (4.330 * Age),
            2
        );
    public static double ActivityFactor =>
        DailyExerciseSessions switch
        {
            <= 1 => 1.2,
            <= 3 => 1.375,
            <= 5 => 1.55,
            <= 7 => 1.725,
            _ => 1.9,
        };
    public static int DailyKcal => (int)Math.Round(BMR * ActivityFactor);

    public static double HeightInSquaredMeters => Height * Height;

    private static void Main()
    {
        Console.WriteLine("Gym Feedback Service");
        Console.WriteLine(new string('=', PadMaxWidth));

        ProcessUser();
    }

    private static double GetRangeInput(string messageBody, double minValue, double maxValue)
    {
        Console.WriteLine($"{messageBody} ({minValue} - {maxValue})");

        double value;
        do
        {
            Console.Write("> ");
        } while (
            !Double.TryParse(Console.ReadLine(), out value) || value < minValue || value > maxValue
        );

        Console.WriteLine();
        return value;
    }

    private static Gender GetGenderInput()
    {
        Console.WriteLine("Choose your gender below:");
        foreach (Gender gender in Enum.GetValues<Gender>())
        {
            Console.WriteLine($"{(int)gender} - {gender}");
        }

        Gender choice;
        do
        {
            Console.Write("> ");
        } while (
            !Enum.TryParse(Console.ReadLine(), true, out choice)
            || !Enum.IsDefined(typeof(Gender), choice)
        );

        Console.WriteLine();
        return choice;
    }

    private static void ProcessUser()
    {
        const double MinWeight = 30;
        const double MaxWeight = 250;
        const double MinHeight = 120;
        const double MaxHeight = 210;
        const int MinAge = 14;
        const int MaxAge = 100;
        const int MaxExerciseSessions = 10;

        Weight = GetRangeInput("Enter your weight in kilograms", MinWeight, MaxWeight);
        Height = GetRangeInput("Enter your height in centimetres", MinHeight, MaxHeight) / 100;
        Age = (int)GetRangeInput("Enter your age in years", MinAge, MaxAge);
        DailyExerciseSessions = (int)GetRangeInput(
            "Enter the number of exercise sessions per day",
            0,
            MaxExerciseSessions
        );
        Gender = GetGenderInput();

        DisplayResults();
    }

    private static void DisplayResults()
    {
        if (WeightDifference == 0)
        {
            Console.WriteLine("You are at your ideal weight!");
        }
        else
        {
            var action = WeightDifference > 0 ? "lose" : "gain";
            Console.WriteLine(
                $"You need to {action} {WeightDifference} kg to reach your target BMI ({IdealBMI})"
            );
        }

        Console.WriteLine(new string('-', PadMaxWidth));
        Console.WriteLine("Summary");
        Console.WriteLine(new string('-', PadMaxWidth));

        PrintRow("Description", "Value");
        PrintRow("Body Mass Index (BMI)", $"{BMI} ({BMICategory})");
        PrintRow("Basal Metabolic Rate (BMR)", $"{BMR} kcal/day");
        PrintRow("Daily caloric requirement to maintain weight", $"{DailyKcal} kcal/day");

        Console.WriteLine(new string('-', PadMaxWidth));
        Console.WriteLine("Thank you for using our Gym Feedback Service!");
        Console.WriteLine();
    }

    private static void PrintRow(string description, string value) =>
        Console.WriteLine($"{description, -PadRightWidth} {value, PadLeftWidth}");
}
