using System.Globalization;
using System.Text;

namespace CurrencyConversion;

internal enum CurrencyIso
{
    Usd,
    Eur,
    Brl,
    Jpy,
    Try
}

internal readonly record struct Currency(decimal ExchangeRate, string Iso)
{
    private RegionInfo Region => new(Iso);

    public NumberFormatInfo NumberFormat => new CultureInfo(Iso).NumberFormat;

    public override string ToString() => $"{Region.CurrencySymbol} {Region.ISOCurrencySymbol} ({Region.CurrencyNativeName})";
}

internal sealed class Program
{
    private const int PadMaxWidth = 40;
    private const int PadRightWidth = 25;
    private const int PadLeftWidth = PadMaxWidth - PadRightWidth - 1;

    private static readonly CurrencyIso[] CurrencyIsOs = Enum.GetValues<CurrencyIso>();
    private static readonly Currency[] Currencies = new Currency[CurrencyIsOs.Length];

    public static decimal Amount { get; private set; }
    public static decimal AmountInChosenCurrency { get; private set; }
    public static decimal TransactionFee { get; private set; }
    public static decimal DiscountAmount { get; private set; }
    public static decimal TotalCostGbp { get; private set; }
    public static Currency ChosenCurrency { get; private set; }

    private static void Main(string[] args)
    {
        Console.WriteLine("Currency Conversion Service");
        Console.WriteLine(new string('=', PadMaxWidth));

        ProcessConversion();
    }

    private static decimal GetGbpAmount()
    {
        const decimal Min = 0;
        const decimal Max = 2_500M;

        Console.WriteLine($"Enter the amount to exchange (greater than {Min:C} and up to {Max:C})");

        decimal amount;
        do
        {
            Console.Write($"> {NumberFormatInfo.CurrentInfo.CurrencySymbol}");
        } while (!Decimal.TryParse(Console.ReadLine(), NumberStyles.Currency, CultureInfo.CurrentCulture, out amount)
              || amount <= Min
              || amount > Max
              || Math.Round(amount, 2) != amount);

        Console.WriteLine();
        return amount;
    }

    private static bool IsStaffMember()
    {
        Console.WriteLine("Is the customer a staff member? (y/n)");
        Console.Write("> ");
        return Console.ReadLine()?.Trim().ToLower() == "y";
    }

    private static CurrencyIso GetCurrencyIsoInput()
    {
        Console.WriteLine();
        Console.WriteLine("Choose a currency to exchange to:");

        for (var i = 0; i < CurrencyIsOs.Length; i++)
        {
            Currency currency = Currencies[i];
            Console.WriteLine($"{i} - {currency}");
        }

        int currencyIndex;
        do
        {
            Console.Write("> ");
        } while (!Int32.TryParse(Console.ReadLine(), out currencyIndex) || currencyIndex < 0 || currencyIndex >= CurrencyIsOs.Length);

        Console.WriteLine();
        return CurrencyIsOs[currencyIndex];
    }

    private static void ProcessConversion()
    {
        const decimal StaffDiscountPercentage = 5M;
        const decimal StaffDiscountRate = StaffDiscountPercentage / 100;

        Amount = GetGbpAmount();

        TransactionFee = Amount switch
                         {
                             > 2_000M => 0.015M,
                             > 1_000M => 0.02M,
                             > 750M => 0.025M,
                             > 300M => 0.03M,
                             _ => 0.035M
                         }
                       * Amount;

        TotalCostGbp = Amount + TransactionFee;

        var isStaffMember = IsStaffMember();
        DiscountAmount = isStaffMember ? Math.Round(TotalCostGbp * StaffDiscountRate, 2) : 0M;
        TotalCostGbp -= DiscountAmount;

        CurrencyIso currencyIso = GetCurrencyIsoInput();
        ChosenCurrency = Currencies[(int)currencyIso];
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

        PrintRow("Total Cost", TotalCostGbp.ToString("C"));

        Console.WriteLine(new string('-', PadMaxWidth));
        Console.WriteLine("Thank you for using our Currency Conversion Service!");
        Console.WriteLine();
    }

    private static void PrintRow(string description, string value) => Console.WriteLine($"{description,-PadRightWidth} {value,PadLeftWidth}");

    static Program()
    {
        Console.OutputEncoding = Encoding.Unicode;
        CultureInfo.CurrentCulture = new("en-GB");

        Currencies[(int)CurrencyIso.Usd] = new(1.40M, "en-US");
        Currencies[(int)CurrencyIso.Eur] = new(1.14M, "fr-FR");
        Currencies[(int)CurrencyIso.Brl] = new(4.77M, "pt-BR");
        Currencies[(int)CurrencyIso.Jpy] = new(151.05M, "ja-JP");
        Currencies[(int)CurrencyIso.Try] = new(5.68M, "tr-TR");
    }
}
