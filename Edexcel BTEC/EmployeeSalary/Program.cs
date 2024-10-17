decimal salary = 0;

for (var week = 1; week <= 4; week++)
{
    int hours;

    Console.WriteLine($"[Week {week + 1}/4]: How many hours has the employee worked? (0 - 100)");

    do
    {
        Console.Write("> ");
    } while (!Int32.TryParse(Console.ReadLine(), out hours) || hours is < 0 or > 100);

    var rate = hours > 40 ? 22.50M : 15.00M;

    salary += hours * rate;

    if (hours > 45)
    {
        salary += 100M;
    }
}

salary *= 0.9M;

Console.WriteLine($"Salary for the month: {salary:C}");
