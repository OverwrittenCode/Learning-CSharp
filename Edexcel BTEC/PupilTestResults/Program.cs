using System.Text;

namespace PupilTestResults;

internal enum GradeType
{
    Fail,
    Pass,
    Merit,
    Distinction
}

internal readonly record struct Result(string Name, uint Score, GradeType Grade);

internal static class Program
{
    private static void Main()
    {
        int numberOfStudents;
        while (true)
        {
            const int Min = 6;
            const int Max = 100;
            Console.Write($"Enter number of students ({Min} - {Max}): ");

            if (Int32.TryParse(Console.ReadLine(), out numberOfStudents) && numberOfStudents is >= Min and <= Max)
            {
                break;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please try again.");
            Console.ResetColor();
        }

        var results = new Result[numberOfStudents];

        for (var i = 0; i < numberOfStudents; i++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[STUDENT {i + 1}]:");
            Console.ResetColor();

            string name;
            while (true)
            {
                Console.Write("Enter student name: ");
                var input = Console.ReadLine()?.Trim();

                if (!String.IsNullOrEmpty(input))
                {
                    name = input;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ResetColor();
            }

            uint score;
            while (true)
            {
                const uint Max = 100;
                Console.Write($"Enter student score (up to {Max}): ");
                if (UInt32.TryParse(Console.ReadLine(), out score) && score <= Max)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ResetColor();
            }

            GradeType grade = score switch
            {
                < 40 => GradeType.Fail,
                <= 50 => GradeType.Pass,
                < 70 => GradeType.Merit,
                _ => GradeType.Distinction
            };

            results[i] = new(name, score, grade);
            Console.WriteLine();
        }

        results = results.OrderByDescending(x => x.Score).ToArray();

        const int NameAlignment = 30;
        const int ScoreAlignment = 10;
        const int GradeAlignment = 20;

        var divider = new string('-', NameAlignment + ScoreAlignment + GradeAlignment);

        StringBuilder text = new StringBuilder().Append($"{"Name",NameAlignment}")
                                                .Append($"{"Score",ScoreAlignment}")
                                                .Append($"{"Grade",GradeAlignment}")
                                                .AppendLine()
                                                .AppendLine(divider);

        foreach ((var name, var score, GradeType grade) in results)
        {
            text.Append($"{name,NameAlignment}").Append($"{score,ScoreAlignment}").AppendLine($"{grade,GradeAlignment}");

            if (grade == GradeType.Distinction)
            {
                Console.WriteLine($"Distinction achieved by {name} with a score of {score}");
            }
        }

        text.AppendLine(divider);

        var contents = text.ToString();
        File.WriteAllText("result.txt", contents);

        Console.WriteLine();
        Console.WriteLine(contents);
        Console.WriteLine($"Results written to {Path.GetFullPath("result.txt")}");
    }
}
