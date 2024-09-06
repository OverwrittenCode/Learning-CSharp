using System.Globalization;
using System.Text;

namespace Edexcel_BTEC_L3_Computing.Unit_4.CurrencyConversion;

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

internal class CurrencyConversion
{
    private const decimal StaffDiscountPercentage = 5M;
    private const decimal StaffDiscountRate = StaffDiscountPercentage / 100;
    private const decimal MinGBPAmount = 0M;
    private const decimal MaxGBPAmount = 2_500M;
    private const int PadMaxWidth = 40;
    private const int PadRightWidth = 25;
    private const int PadLeftWidth = PadMaxWidth - PadRightWidth - 1;

    private static readonly CurrencyISO[] CurrencyISOs = Enum.GetValues<CurrencyISO>();
    private static readonly Currency[] Currencies = new Currency[CurrencyISOs.Length];

    public decimal Amount { get; private set; }
    public decimal AmountInChosenCurrency { get; private set; }
    public decimal TransactionFee { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public decimal TotalCostGBP { get; private set; }
    public Currency ChosenCurrency { get; private set; }

    static CurrencyConversion()
    {
        Console.OutputEncoding = Encoding.Unicode;
        CultureInfo.CurrentCulture = new CultureInfo("en-GB");

        Currencies[(int)CurrencyISO.USD] = new Currency(1.40M, "en-US");
        Currencies[(int)CurrencyISO.EUR] = new Currency(1.14M, "fr-FR");
        Currencies[(int)CurrencyISO.BRL] = new Currency(4.77M, "pt-BR");
        Currencies[(int)CurrencyISO.JPY] = new Currency(151.05M, "ja-JP");
        Currencies[(int)CurrencyISO.TRY] = new Currency(5.68M, "tr-TR");
    }

    public static void Run()
    {
        Console.WriteLine("Currency Conversion Service");
        Console.WriteLine(new string('=', PadMaxWidth));

        CurrencyConversion currencyConversion = new();
        currencyConversion.ProcessConversion();
    }

    private static decimal GetGBPAmount()
    {
        Console.WriteLine(
            $"Enter the amount to exchange (greater than {MinGBPAmount:C} and up to {MaxGBPAmount:C})"
        );

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
            || amount <= MinGBPAmount
            || amount > MaxGBPAmount
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

    private void ProcessConversion()
    {
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

    private void DisplayTransactionDetails()
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
        Console.WriteLine($"{description, -PadRightWidth} {value, PadLeftWidth}");
}
