namespace Edexcel_BTEC_L3_Computing.Unit_4.EliteAthletics;

internal enum Gender
{
    Male,
    Female,
}

internal enum RecordBreaker
{
    World,
    European,
    British,
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

    public void AddRow(params object[] rows)
    {
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

    public void Print()
    {
        var divider = new string(RowSeparator, _paddings.Sum() + _paddings.Length + 1);

        Console.WriteLine(divider);

        foreach (object[] rows in Rows)
        {
            int padIndex = 0;

            string value = rows.Aggregate(
                "",
                (acc, current) =>
                    acc + CellSeparator + current.ToString()?.PadRight(_paddings[padIndex++]),
                result => result + CellSeparator
            );

            Console.WriteLine(value);
            Console.WriteLine(divider);
        }
    }
}

internal sealed class EliteAthletics
{
    private const int MinAthletes = 4;
    private const int MaxAthletes = 8;
    private const double MinTime = 6;
    private const double MaxTime = 30;

    private static readonly Gender[] Genders = Enum.GetValues<Gender>();
    private static readonly RecordBreaker[] RecordBreakers = Enum.GetValues<RecordBreaker>();

    private static readonly double[,] Records = new double[Genders.Length, RecordBreakers.Length];

    private readonly List<double> _times = [];

    private Gender _genderGroup;

    static EliteAthletics()
    {
        Records[(int)Gender.Male, (int)RecordBreaker.World] = 9.58;
        Records[(int)Gender.Male, (int)RecordBreaker.European] = 9.86;
        Records[(int)Gender.Male, (int)RecordBreaker.British] = 9.87;

        Records[(int)Gender.Female, (int)RecordBreaker.World] = 10.49;
        Records[(int)Gender.Female, (int)RecordBreaker.European] = 10.73;
        Records[(int)Gender.Female, (int)RecordBreaker.British] = 10.99;
    }

    public static void Run()
    {
        new EliteAthletics().Main();
    }

    private void Main()
    {
        foreach (Gender option in Genders)
        {
            Console.WriteLine($"{(int)option} - {option}");
        }

        Console.WriteLine("Pick the gender for the group of althete");

        do
        {
            Console.Write("> ");
        } while (
            !Enum.TryParse(Console.ReadLine(), true, out _genderGroup)
            || !Enum.IsDefined(typeof(Gender), _genderGroup)
        );

        while (_times.Count < MaxAthletes)
        {
            if (_times.Count >= MinAthletes)
            {
                Console.WriteLine("Continue? (y/n)");
                Console.Write("> ");

                if (Console.ReadLine()?.ToLower().Trim() != "y")
                {
                    break;
                }
            }

            ProcessAlthlete();
        }

        DisplaySummary();
    }

    private void ProcessAlthlete()
    {
        Console.WriteLine();
        Console.WriteLine($"Lane {_times.Count + 1}/{MaxAthletes}");
        Console.WriteLine(
            $"Enter time taken for the athlete to finish their 100m race (from {MinTime} to {MaxTime} seconds)"
        );

        double time;

        do
        {
            Console.Write("> ");
        } while (!Double.TryParse(Console.ReadLine(), out time) || time is < MinTime or > MaxTime);

        _times.Add(Double.Round(time, 2));
    }

    private void DisplaySummary()
    {
        _times.Sort();

        List<(double Time, int Lane, RecordBreaker type)> recordBreakers = [];

        Console.WriteLine("Summary");

        Table timeTable = new("Lane", "Time (s)");

        for (int i = 0; i < _times.Count; i++)
        {
            if (GetRecordBreaker(_times[i]) is RecordBreaker recordBreaker)
            {
                recordBreakers.Add((_times[i], i + 1, recordBreaker));
            }

            timeTable.AddRow(i + 1, _times[i]);
        }

        timeTable.Print();

        foreach (var (time, lane, type) in recordBreakers)
        {
            Console.WriteLine();
            Console.WriteLine(
                $"{type.ToString().ToUpper()} RECORD BROKEN BY ALTHELETE LANE {lane}"
            );

            Table recordTable = new("Old Record", "New Record");
            recordTable.AddRow(Records[(int)_genderGroup, (int)type], time);
            recordTable.Print();
        }

        Console.WriteLine();
    }

    private RecordBreaker? GetRecordBreaker(double time)
    {
        for (int i = 0; i < Records.GetLength(1); i++)
        {
            if (Records[(int)_genderGroup, i] > time)
            {
                return (RecordBreaker)i;
            }
        }

        return null;
    }
}
