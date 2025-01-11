using System.Globalization;

CultureInfo.CurrentCulture = new("en-GB");

var salary = 0m;

for (var week = 1; week <= 4; week++)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"[WEEK {week}/4]");
    Console.ResetColor();

    int hours;
    while (true)
    {
        const int MaxHours = 100;
        Console.Write($"How many hours has the employee worked (up to {MaxHours})?: ");

        var input = Console.ReadLine();
        Console.WriteLine();

        if (Int32.TryParse(input, out hours) && hours is > 0 and <= MaxHours)
        {
            break;
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid input. Try again.");
        Console.ResetColor();
    }

    var rate = hours > 40 ? 22.50m : 15.00m;

    salary += hours * rate;

    if (hours > 45)
    {
        salary += 100m;
    }
}

var tax = salary * 0.10m;
salary -= tax;

Console.Write("Salary for the month: ");
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"{salary:C}");
Console.ResetColor();
