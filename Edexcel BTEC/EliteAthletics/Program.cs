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

internal sealed class Table
{
    private const char CellSeparator = '|';
    private const char RowSeparator = '-';

    private readonly int[] _paddings;

    public List<object[]> Rows { get; }

    public Table(params string[] headers)
    {
        Rows = [headers];
        _paddings = headers.Select(header => header.Length + 5).ToArray();
    }

    public void AddRow(params object[] rows)
    {
        for (var i = 0; i < rows.Length; i++)
        {
            var length = rows[i].ToString()?.Length + 5 ?? throw new ArgumentException("ToString method returned null", nameof(rows));

            if (length > _paddings[i])
            {
                _paddings[i] = length;
            }
        }

        Rows.Add(rows);
    }

    public void Print()
    {
        var divider = new string(RowSeparator, _paddings.Sum() + _paddings.Length + 1);

        Console.WriteLine(divider);

        foreach (object[] rows in Rows)
        {
            var padIndex = 0;

            var value = rows.Aggregate("", (acc, current) => acc + CellSeparator + current.ToString()?.PadRight(_paddings[padIndex++]), result => result + CellSeparator);

            Console.WriteLine(value);
            Console.WriteLine(divider);
        }
    }
}

internal sealed class Program
{
    private const int MinAthletes = 4;
    private const int MaxAthletes = 8;

    private static readonly Gender[] Genders = Enum.GetValues<Gender>();
    private static readonly RecordBreaker[] RecordBreakers = Enum.GetValues<RecordBreaker>();

    private static readonly double[,] Records = new double[Genders.Length, RecordBreakers.Length];

    private static readonly List<double> Times = [];

    private static Gender GenderGroup;

    private static void Main()
    {
        foreach (Gender option in Genders)
        {
            Console.WriteLine($"{(int)option} - {option}");
        }

        Console.WriteLine("Pick the gender for the group of athletes");

        do
        {
            Console.Write("> ");
        } while (!Enum.TryParse(Console.ReadLine(), true, out GenderGroup) || !Enum.IsDefined(typeof(Gender), GenderGroup));

        while (Times.Count < MaxAthletes)
        {
            if (Times.Count >= MinAthletes)
            {
                Console.WriteLine("Continue? (y/n)");
                Console.Write("> ");

                if (Console.ReadLine()?.ToLower().Trim() != "y")
                {
                    break;
                }
            }

            ProcessAthlete();
        }

        DisplaySummary();
    }

    private static void ProcessAthlete()
    {
        const double MinTime = 6;
        const double MaxTime = 30;

        Console.WriteLine();
        Console.WriteLine($"Lane {Times.Count + 1}/{MaxAthletes}");
        Console.WriteLine($"Enter time taken for the athlete to finish their 100m race (from {MinTime} to {MaxTime} seconds)");

        double time;

        do
        {
            Console.Write("> ");
        } while (!Double.TryParse(Console.ReadLine(), out time) || time is < MinTime or > MaxTime);

        Times.Add(Double.Round(time, 2));
    }

    private static void DisplaySummary()
    {
        Times.Sort();

        List<(double Time, int Lane, RecordBreaker type)> recordBreakers = [];

        Console.WriteLine("Summary");

        Table timeTable = new("Lane", "Time (s)");

        for (var i = 0; i < Times.Count; i++)
        {
            if (GetRecordBreaker(Times[i]) is { } recordBreaker)
            {
                recordBreakers.Add((Times[i], i + 1, recordBreaker));
            }

            timeTable.AddRow(i + 1, Times[i]);
        }

        timeTable.Print();

        foreach (var (time, lane, type) in recordBreakers)
        {
            Console.WriteLine();
            Console.WriteLine($"{type.ToString().ToUpper()} RECORD BROKEN BY ALTHELETE LANE {lane}");

            Table recordTable = new("Old Record", "New Record");
            recordTable.AddRow(Records[(int)GenderGroup, (int)type], time);
            recordTable.Print();
        }

        Console.WriteLine();
    }

    private static RecordBreaker? GetRecordBreaker(double time)
    {
        for (var i = 0; i < Records.GetLength(1); i++)
        {
            if (Records[(int)GenderGroup, i] > time)
            {
                return (RecordBreaker)i;
            }
        }

        return null;
    }

    static Program()
    {
        Records[(int)Gender.Male, (int)RecordBreaker.World] = 9.58;
        Records[(int)Gender.Male, (int)RecordBreaker.European] = 9.86;
        Records[(int)Gender.Male, (int)RecordBreaker.British] = 9.87;

        Records[(int)Gender.Female, (int)RecordBreaker.World] = 10.49;
        Records[(int)Gender.Female, (int)RecordBreaker.European] = 10.73;
        Records[(int)Gender.Female, (int)RecordBreaker.British] = 10.99;
    }
}
