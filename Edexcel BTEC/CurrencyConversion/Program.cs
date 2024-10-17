using System.Globalization;
using System.Text;

namespace CurrencyConversion;

internal enum CurrencyISO
{
    USD,
    EUR,
    BRL,
    JPY,
    TRY,
}

internal readonly record struct Currency(decimal ExchangeRate, string ISO)
{
    private RegionInfo Region => new(ISO);

    public override string ToString()
    {
        return $"{Region.CurrencySymbol} {Region.ISOCurrencySymbol} ({Region.CurrencyNativeName})";
    }

    public NumberFormatInfo NumberFormat => new CultureInfo(ISO).NumberFormat;
}

internal sealed class Program
{
    private const int PadMaxWidth = 40;
    private const int PadRightWidth = 25;
    private const int PadLeftWidth = PadMaxWidth - PadRightWidth - 1;

    private static readonly CurrencyISO[] CurrencyISOs = Enum.GetValues<CurrencyISO>();
    private static readonly Currency[] Currencies = new Currency[CurrencyISOs.Length];

    public static decimal Amount { get; private set; }
    public static decimal AmountInChosenCurrency { get; private set; }
    public static decimal TransactionFee { get; private set; }
    public static decimal DiscountAmount { get; private set; }
    public static decimal TotalCostGBP { get; private set; }
    public static Currency ChosenCurrency { get; private set; }

    static Program()
    {
        Console.OutputEncoding = Encoding.Unicode;
        CultureInfo.CurrentCulture = new CultureInfo("en-GB");

        Currencies[(int)CurrencyISO.USD] = new Currency(1.40M, "en-US");
        Currencies[(int)CurrencyISO.EUR] = new Currency(1.14M, "fr-FR");
        Currencies[(int)CurrencyISO.BRL] = new Currency(4.77M, "pt-BR");
        Currencies[(int)CurrencyISO.JPY] = new Currency(151.05M, "ja-JP");
        Currencies[(int)CurrencyISO.TRY] = new Currency(5.68M, "tr-TR");
    }

    private static void Main(string[] args)
    {
        Console.WriteLine("Currency Conversion Service");
        Console.WriteLine(new string('=', PadMaxWidth));

        ProcessConversion();
    }

    private static decimal GetGBPAmount()
    {
        const decimal Min = 0;
        const decimal Max = 2_500M;

        Console.WriteLine($"Enter the amount to exchange (greater than {Min:C} and up to {Max:C})");

        decimal amount;
        do
        {
            Console.Write($"> {NumberFormatInfo.CurrentInfo.CurrencySymbol}");
        } while (
            !Decimal.TryParse(
                Console.ReadLine(),
                NumberStyles.Currency,
                CultureInfo.CurrentCulture,
                out amount
            )
            || amount <= Min
            || amount > Max
            || Math.Round(amount, 2) != amount
        );

        Console.WriteLine();
        return amount;
    }

    private static bool IsStaffMember()
    {
        Console.WriteLine("Is the customer a staff member? (y/n)");
        Console.Write("> ");
        return Console.ReadLine()?.Trim().ToLower() == "y";
    }

    private static CurrencyISO GetCurrencyISOInput()
    {
        Console.WriteLine();
        Console.WriteLine("Choose a currency to exchange to:");

        for (int i = 0; i < CurrencyISOs.Length; i++)
        {
            Currency currency = Currencies[i];
            Console.WriteLine($"{i} - {currency}");
        }

        int currencyIndex;
        do
        {
            Console.Write("> ");
        } while (
            !Int32.TryParse(Console.ReadLine(), out currencyIndex)
            || currencyIndex < 0
            || currencyIndex >= CurrencyISOs.Length
        );

        Console.WriteLine();
        return CurrencyISOs[currencyIndex];
    }

    private static void ProcessConversion()
    {
        const decimal StaffDiscountPercentage = 5M;
        const decimal StaffDiscountRate = StaffDiscountPercentage / 100;

        Amount = GetGBPAmount();

        TransactionFee =
            Amount switch
            {
                > 2_000M => 0.015M,
                > 1_000M => 0.02M,
                > 750M => 0.025M,
                > 300M => 0.03M,
                _ => 0.035M,
            } * Amount;

        TotalCostGBP = Amount + TransactionFee;

        bool isStaffMember = IsStaffMember();
        DiscountAmount = isStaffMember ? Math.Round(TotalCostGBP * StaffDiscountRate, 2) : 0M;
        TotalCostGBP -= DiscountAmount;

        CurrencyISO currencyISO = GetCurrencyISOInput();
        ChosenCurrency = Currencies[(int)currencyISO];
        AmountInChosenCurrency = Math.Round(Amount * ChosenCurrency.ExchangeRate, 2);

        DisplayTransactionDetails();
    }

    private static void DisplayTransactionDetails()
    {
        Console.WriteLine($"Customer requests to convert {Amount:C} to {ChosenCurrency}");

        Console.WriteLine(new string('-', PadMaxWidth));
        Console.WriteLine("Transaction Details");
        Console.WriteLine(new string('-', PadMaxWidth));

        PrintRow("Description", "Value");
        PrintRow("Amount To Convert", Amount.ToString("C"));
        PrintRow("Exchanged To", AmountInChosenCurrency.ToString("C", ChosenCurrency.NumberFormat));
        PrintRow("Transaction Fee", TransactionFee.ToString("C"));

        if (DiscountAmount > 0)
        {
            PrintRow("Staff Discount Applied", (-DiscountAmount).ToString("C"));
        }

        PrintRow("Total Cost", TotalCostGBP.ToString("C"));

        Console.WriteLine(new string('-', PadMaxWidth));
        Console.WriteLine("Thank you for using our Currency Conversion Service!");
        Console.WriteLine();
    }

    private static void PrintRow(string description, string value) =>
        Console.WriteLine($"{description,-PadRightWidth} {value,PadLeftWidth}");
}
