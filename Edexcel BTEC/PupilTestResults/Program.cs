namespace PupilTestResults;

internal enum Grade
{
    Fail,
    Pass,
    Merit,
    Distinction,
}

internal readonly record struct Student(string Name, int Score)
{
    public const int MinQuantity = 6;
    public const int MaxQuantity = 60;
    public const int MaxNameLength = 30;
    public const int MaxScore = 100;

    public Grade Grade =>
        Score switch
        {
            < 40 => Grade.Fail,
            <= 50 => Grade.Pass,
            < 70 => Grade.Merit,
            _ => Grade.Distinction,
        };
}

internal sealed class Table
{
    private const char CellSeparator = '|';
    private const char RowSeparator = '-';

    private readonly int[] _paddings;

    public List<object[]> Rows { get; private set; }

    public Table(params string[] headers)
    {
        Rows = [headers];
        _paddings = headers.Select(header => header.Length + 5).ToArray();
    }

    public override string ToString()
    {
        var result = new System.Text.StringBuilder();

        var divider = new string(RowSeparator, _paddings.Sum() + _paddings.Length + 1);
        result.AppendLine(divider);

        foreach (object[] row in Rows)
        {
            int padIndex = 0;
            string value = row.Aggregate(
                "",
                (acc, current) =>
                    acc + CellSeparator + current.ToString()?.PadRight(_paddings[padIndex++]),
                result => result + CellSeparator
            );

            result.AppendLine(value);
            result.AppendLine(divider);
        }

        return result.ToString();
    }

    public void AddRow(params object[] rows)
    {
        if (rows.Length != _paddings.Length)
        {
            throw new ArgumentException(
                "Row length does not match the number of columns.",
                nameof(rows)
            );
        }

        for (int i = 0; i < rows.Length; i++)
        {
            int length =
                rows[i].ToString()?.Length + 5
                ?? throw new ArgumentException("ToString method returned null", nameof(rows));

            if (length > _paddings[i])
            {
                _paddings[i] = length;
            }
        }

        Rows.Add(rows);
    }
}

internal sealed class Program
{
    private static readonly int StudentCount;
    private static readonly Student[] Students;

    static Program()
    {
        Console.WriteLine(
            $"Enter the number of students (from {Student.MinQuantity} to {Student.MaxQuantity})"
        );

        do
        {
            Console.Write("> ");
        } while (
            !Int32.TryParse(Console.ReadLine(), out StudentCount)
            || StudentCount is < Student.MinQuantity or > Student.MaxQuantity
        );

        Students = new Student[StudentCount];
    }

    private static void Main()
    {
        CollectData();
        HandleData();
    }

    private static void CollectData()
    {
        for (int i = 0; i < StudentCount; i++)
        {
            Console.WriteLine($"[STUDENT {i + 1}/{StudentCount}]");

            string name;
            int score;

            Console.WriteLine($"Enter name of student (up to {Student.MaxNameLength} characters)");

            do
            {
                Console.Write("> ");
            } while (
                Console.ReadLine() is not string input
                || (name = input).Length is <= 0 or > Student.MaxNameLength
            );

            Console.WriteLine($"Enter score for student (up to {Student.MaxScore})");

            do
            {
                Console.Write("> ");
            } while (
                !Int32.TryParse(Console.ReadLine(), out score) || score is < 0 or > Student.MaxScore
            );

            Students[i] = new(name, score);
        }
    }

    private static void HandleData()
    {
        Table table = new("Name", "Score", "Grade");

        Student[] students = [.. Students.OrderByDescending(student => student.Score)];

        foreach (Student student in students)
        {
            table.AddRow(student.Name, student.Score, student.Grade);
        }

        var result = table.ToString();

        Console.WriteLine(result);

        if (students[0].Grade == Grade.Distinction)
        {
            Console.WriteLine("Distinction has been achieved.");
        }

        var relativePath = @"..\..\..\results.txt";

        File.WriteAllText(relativePath, result);

        Console.WriteLine($"Results saved to {Path.GetFullPath(relativePath)}");
    }
}
