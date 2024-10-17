namespace SchoolSportsDay;

public enum Group
{
    Red,
    Green,
    White,
}

public sealed class Program
{
    private const int PupilCount = 10;
    private const int BirthMonthMinValue = 1;
    private const int BirthMonthMaxValue = 12;
    private const int PadMaxWidth = 25;
    private const int PadRightWidth = 10;
    private const int PadLeftWidth = PadMaxWidth - PadRightWidth - 1;

    private static readonly int GroupLength = Enum.GetValues<Group>().Length;

    private readonly int[] _groupedPupilCounters = new int[GroupLength];

    private static void Main()
    {
        Console.WriteLine("School Sports Day Service");
        Console.WriteLine(new string('=', PadMaxWidth));

        Program schoolSportsDay = new();
        schoolSportsDay.CollectPupilData();
        schoolSportsDay.DisplayResults();
    }

    private void CollectPupilData()
    {
        for (var i = 0; i < PupilCount; i++)
        {
            var birthMonth = GetBirthMonthInput(i + 1);
            Group group = AssignGroup(birthMonth);
            _groupedPupilCounters[(int)group]++;
        }
    }

    private static int GetBirthMonthInput(int pupilNumber)
    {
        Console.WriteLine(
            $"[Pupil {pupilNumber}]: Enter their birth month ({BirthMonthMinValue} - {BirthMonthMaxValue})"
        );

        int birthMonth;
        do
        {
            Console.Write("> ");
        } while (
            !Int32.TryParse(Console.ReadLine(), out birthMonth)
            || birthMonth < BirthMonthMinValue
            || birthMonth > BirthMonthMaxValue
        );

        Console.WriteLine();
        return birthMonth;
    }

    private static Group AssignGroup(int birthMonth) =>
        birthMonth switch
        {
            <= 4 => Group.Red,
            <= 8 => Group.Green,
            _ => Group.White,
        };

    private void DisplayResults()
    {
        Console.WriteLine(new string('-', PadMaxWidth));
        Console.WriteLine("Summary");
        Console.WriteLine(new string('-', PadMaxWidth));

        PrintRow("Group", "Pupil Count");

        foreach (Group group in Enum.GetValues<Group>())
        {
            var pupilCount = _groupedPupilCounters[(int)group];
            PrintRow(group.ToString(), pupilCount.ToString());
        }

        Console.WriteLine(new string('-', PadMaxWidth));
        Console.WriteLine("Thank you for using our School Sports Day Service!");
        Console.WriteLine();
    }

    private static void PrintRow(string description, string value) =>
        Console.WriteLine($"{description, -PadRightWidth} {value, PadLeftWidth}");
}
