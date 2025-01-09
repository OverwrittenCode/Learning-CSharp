using System.Globalization;

namespace CurrencyConversion;

internal enum CurrencyType
{
    USD,
    EUR,
    BRL,
    JPY,
    TRY
}

internal static class Program
{
    private static readonly string CurrencyOptions = String.Join(", ", Enum.GetValues<CurrencyType>());

    private static void Main()
    {
        CultureInfo.CurrentCulture = new("en-GB");

        decimal gbpAmount;

        while (true)
        {
            const decimal MaxAmountInPounds = 2_500;

            Console.Write($"Enter amount to convert in GBP (Up to {MaxAmountInPounds:C}): £");

            var input = Console.ReadLine();
            Console.WriteLine();

            if (Decimal.TryParse(input, NumberStyles.Currency, CultureInfo.InvariantCulture, out gbpAmount) && gbpAmount is > 0 and <= MaxAmountInPounds)
            {
                break;
            }

            Console.WriteLine($"Amount must be above {0:C} and cannot exceed {MaxAmountInPounds:C}, please try again.");
            Console.WriteLine();
        }

        CurrencyType requestedCurrency;

        while (true)
        {
            Console.WriteLine(CurrencyOptions);
            Console.Write("Enter the currency to convert to: ");

            var input = Console.ReadLine()?.Trim();
            Console.WriteLine();

            if (Enum.TryParse(input, true, out requestedCurrency) && Enum.IsDefined(requestedCurrency))
            {
                break;
            }

            Console.WriteLine("Unknown currency, please try again.");
            Console.WriteLine();
        }

        Console.Write("Is the customer a staff? (y/n): ");
        var isCustomerStaff = Console.ReadLine()?.Equals("y", StringComparison.CurrentCultureIgnoreCase);
        Console.WriteLine();

        var fee = gbpAmount switch
        {
            <= 300 => 0.035m,
            <= 750 => 0.03m,
            <= 1_000 => 0.025m,
            <= 2_000 => 0.02m,
            _ => 0.015m
        };

        var rate = requestedCurrency switch
        {
            CurrencyType.USD => 1.40m,
            CurrencyType.EUR => 1.14m,
            CurrencyType.BRL => 4.77m,
            CurrencyType.JPY => 151.05m,
            CurrencyType.TRY => 5.68m
        };

        var amountInRequestedCurrency = gbpAmount * rate;
        var transactionFee = gbpAmount * fee;
        var discount = isCustomerStaff == true ? 0.05m : 0m;
        var discountAmount = transactionFee * discount;
        var totalCost = transactionFee - discountAmount;

        const int Alignment = -20;

        Console.WriteLine(
            $"""
             {"Converted Amount",Alignment} {amountInRequestedCurrency:N2} {requestedCurrency,Alignment}
             {"Transaction Fee",Alignment} {transactionFee,Alignment:C}
             {"Discount",Alignment} {-discountAmount,Alignment:C}
             {"Total",Alignment} {totalCost,Alignment:C}
             """
        );
    }
}
