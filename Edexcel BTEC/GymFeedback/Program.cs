using System.Globalization;

namespace GymFeedback;

internal enum Gender
{
    Male,
    Female
}

internal static class Program
{
    private static readonly string GenderOptions = String.Join(", ", Enum.GetValues<Gender>());

    private static void Main()
    {
        CultureInfo.CurrentCulture = new("en-GB");

        Gender genderGroup;
        while (true)
        {
            Console.WriteLine(GenderOptions);
            Console.Write("Enter gender: ");
            var input = Console.ReadLine()?.Trim();
            Console.WriteLine();

            if (Enum.TryParse(input, true, out genderGroup) && Enum.IsDefined(genderGroup))
            {
                break;
            }

            DisplayErrorMessage();
        }

        double weightKg;
        while (true)
        {
            const int Min = 30;
            const int Max = 250;

            Console.Write($"Enter weight in kg ({Min} - {Max}): ");
            var input = Console.ReadLine()?.Replace("kg", "");
            Console.WriteLine();

            if (Double.TryParse(input, out weightKg) && weightKg is >= Min and <= Max)
            {
                break;
            }

            DisplayErrorMessage();
        }

        double heightCm;
        while (true)
        {
            const int Min = 120;
            const int Max = 210;

            Console.Write($"Enter height in cm ({Min} - {Max}): ");
            var input = Console.ReadLine()?.Replace("cm", "");
            Console.WriteLine();

            if (Double.TryParse(input, out heightCm) && heightCm is >= Min and <= Max)
            {
                break;
            }

            DisplayErrorMessage();
        }

        int age;
        while (true)
        {
            const int Min = 14;
            const int Max = 100;

            Console.Write($"Enter age in years ({Min} - {Max}): ");
            var input = Console.ReadLine();
            Console.WriteLine();

            if (Int32.TryParse(input, out age) && age is >= Min and <= Max)
            {
                break;
            }

            DisplayErrorMessage();
        }

        var bmr = genderGroup switch
        {
            Gender.Male => 88.362 + (13.397 * weightKg) + (4.799 * heightCm) - (5.677 * age),
            Gender.Female => 447.593 + (9.247 * weightKg) + (3.098 * heightCm) - (4.330 * age)
        };

        var heightMetres = heightCm / 100;
        var heightSquaredMetres = heightMetres * heightMetres;
        var bmi = weightKg / heightSquaredMetres;

        const int IdealBmi = 22;
        var idealWeight = IdealBmi * heightSquaredMetres;
        var weightFromIdeal = idealWeight - weightKg;

        var bmiCategory = bmi switch
        {
            < 18.5 => "Underweight",
            < 25 => "Normal",
            < 30 => "Overweight",
            _ => "Obesity"
        };

        uint dailyExerciseSessions;
        while (true)
        {
            Console.Write("Enter the number of daily exercise sessions: ");
            var input = Console.ReadLine();
            Console.WriteLine();

            if (UInt32.TryParse(input, out dailyExerciseSessions))
            {
                break;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please try again.");
            Console.ResetColor();
        }

        var requiredDailyKcal = bmr
                              * dailyExerciseSessions switch
                                {
                                    < 1 => 1.2,
                                    < 3 => 1.375,
                                    < 5 => 1.55,
                                    < 7 => 1.725,
                                    _ => 1.9
                                };

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[SUMMARY]");
        Console.ResetColor();
        Console.WriteLine($"Required daily kilocalories to maintain current weight: {requiredDailyKcal:N0}kcal");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("[BREAKDOWN]");
        Console.ResetColor();
        Console.WriteLine(
            $"""
             {"Body Mass Index (BMI)",-15}: {bmi:N1} ({bmiCategory})
             {"Basal Metabolic Rate (BMR)",-15} {bmr:N2}

             {"Weight",-15}: {weightKg:N2}kg
             {"Ideal Weight",-15}: {idealWeight:N2}kg
             {"Difference",-15} {weightFromIdeal:N2}kg
             """
        );
        return;

        void DisplayErrorMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please try again.");
            Console.ResetColor();
        }
    }
}
