using System.Globalization;

decimal loan;
Console.WriteLine($"Enter the amount (GBP) you would like to withdraw for your loan");

do
{
    Console.Write($"> {CultureInfo.CurrentCulture.NumberFormat.CurrencySymbol}");
} while (
    !Decimal.TryParse(
        Console.ReadLine(),
        NumberStyles.Currency,
        CultureInfo.CurrentCulture,
        out loan
    )
    || loan < 0
);

int months;
Console.WriteLine("Enter number of months to repay loan over (includes interest)");

do
{
    Console.Write("> ");
} while (!Int32.TryParse(Console.ReadLine(), out months) || months is < 1 or > 12);

Console.WriteLine("Will you make an early payment? (y/n");
if (Console.ReadLine()?.ToLower().Trim() == "y")
{
    loan *= 0.9M;
}

var monthlyCharge = loan * 1.05M / months;

Console.WriteLine($"Total cost to pay back over months ({months}): {monthlyCharge:C}");
