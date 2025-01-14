using System.Globalization;

CultureInfo.CurrentCulture = new("en-GB");

decimal loan;
while (true)
{
    Console.Write("Enter the amount (GBP) you would like to withdraw for your loan: £");
    var input = Console.ReadLine();
    Console.WriteLine();
    if (Decimal.TryParse(input, NumberStyles.Currency, CultureInfo.CurrentCulture, out loan) && loan > 0)
    {
        break;
    }

    DisplayErrorMessage();
}

int months;
while (true)
{
    Console.Write("Enter number of months to repay loan over (includes interest): ");
    var input = Console.ReadLine();
    Console.WriteLine();

    if (Int32.TryParse(input, out months) && months is >= 1 and <= 12)
    {
        break;
    }

    DisplayErrorMessage();
}

Console.Write("Will you make an early payment? (y/n): ");
if (Console.ReadLine()?.ToLower().Trim() == "y")
{
    loan *= 0.9M;
}

var monthlyCharge = loan * 1.05M / months;
Console.WriteLine($"Total cost to pay back over months ({months}): {monthlyCharge:C}");
return;

void DisplayErrorMessage()
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Invalid input. Please try again.");
    Console.ResetColor();
}
