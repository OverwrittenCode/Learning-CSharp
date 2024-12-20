using System.Text.RegularExpressions;

namespace PizzaOrder;

internal enum Size
{
    Small,
    Medium,
    Large
}

internal readonly record struct Pizza(Size Size, int ExtraToppingsAmount = 0)
{
    public const int MinQuantity = 1;
    public const int MaxQuantity = 6;
    public const int MaxToppings = 15;

    public static readonly Size[] Sizes = Enum.GetValues<Size>();
    public static readonly decimal[] SizeCosts = new decimal[Sizes.Length];
    public static readonly decimal[] ExtraToppingsCosts = [0, 0.75M, 1.35M, 2.00M, 2.50M];

    static Pizza()
    {
        SizeCosts[(int)Size.Small] = 3.25M;
        SizeCosts[(int)Size.Medium] = 5.50M;
        SizeCosts[(int)Size.Large] = 7.15M;
    }

    public decimal CostOfSize => SizeCosts[(int)Size];

    public decimal CostOfExtraToppings => ExtraToppingsCosts[Int32.Min(ExtraToppingsAmount, ExtraToppingsCosts.Length - 1)];

    public decimal Cost => CostOfSize + CostOfExtraToppings;
}

internal sealed class Table
{
    private const char CellSeparator = '|';
    private const char RowSeparator = '-';

    private readonly int[] _paddings;

    public List<object[]> Rows { get; }

    public Table(params string[] headers)
    {
        Rows = [headers];
        _paddings = headers.Select(header => header.Length + 5).ToArray();
    }

    public void AddRow(params object[] rows)
    {
        if (rows.Length != _paddings.Length)
        {
            throw new ArgumentException("Row length does not match the number of columns.", nameof(rows));
        }

        for (var i = 0; i < rows.Length; i++)
        {
            var length = rows[i].ToString()?.Length + 5 ?? throw new ArgumentException("ToString method returned null", nameof(rows));

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

        foreach (object[] row in Rows)
        {
            var padIndex = 0;
            var value = row.Aggregate("", (acc, current) => acc + CellSeparator + current.ToString()?.PadRight(_paddings[padIndex++]), result => result + CellSeparator);

            Console.WriteLine(value);
            Console.WriteLine(divider);
        }
    }
}

internal sealed partial class Program
{
    private const int MaxNameLength = 30;
    private const int MaxAddressLength = 80;
    private const decimal DiscountRate = 0.10M;
    private const decimal DiscountFrom = 20M;
    private const decimal DeliveryCost = 2.50M;
    public static List<Pizza> Pizzas { get; } = [];

    public static string Name { get; private set; } = "";
    public static string Address { get; private set; } = "";
    public static string PhoneNumber { get; private set; } = "";
    public static bool IsDelivery { get; private set; }

    [GeneratedRegex(@"^0?((7\d{9})|(1\d{8,9})|(2\d{8,9})|(3\d{8,9})|(8\d{8,9}))$")]
    private static partial Regex PhoneNumberRegex();

    private static void Main()
    {
        ProcessCustomerDetails();

        while (Pizzas.Count < Pizza.MaxQuantity)
        {
            Console.WriteLine();

            if (Pizzas.Count >= Pizza.MinQuantity)
            {
                Console.WriteLine("Add another pizza? (y/n)");
                Console.Write("> ");

                if (Console.ReadLine()?.Trim().ToLower() != "y")
                {
                    break;
                }

                Console.WriteLine();
            }

            ProcessPizza();
        }

        DisplaySummary();
    }

    private static void ProcessCustomerDetails()
    {
        Console.WriteLine($"Please enter the name on the order (up to {MaxNameLength} characters)");

        do
        {
            Console.Write("> ");
        } while (Console.ReadLine() is not { } input || (Name = input).Length is < 1 or > MaxNameLength);

        Console.WriteLine($"Please enter an address to receive the pizza (up to {MaxAddressLength} characters)");

        do
        {
            Console.Write("> ");
        } while (Console.ReadLine() is not { } input || (Address = input).Length is < 1 or > MaxAddressLength);

        Console.WriteLine("Please enter a phone number for updates");

        do
        {
            Console.Write("> +44 ");
        } while (Console.ReadLine() is not { } input || !PhoneNumberRegex().IsMatch(PhoneNumber = input.Replace(" ", "")));

        Console.WriteLine("Do you want this to be delivered? (y/n)");

        Console.Write("> ");
        IsDelivery = Console.ReadLine()?.Trim().ToLower() == "y";

        Console.WriteLine();
    }

    private static void ProcessPizza()
    {
        Console.WriteLine($"Pizza {Pizzas.Count + 1}/{Pizza.MaxQuantity}");
        Console.WriteLine(new string('-', 20));

        foreach (Size option in Pizza.Sizes)
        {
            var index = (int)option;

            Console.WriteLine($"{index} - {option} ({Pizza.SizeCosts[index]:C})");
        }

        Console.WriteLine();

        Console.WriteLine("Pick a pizza size from the options above");

        Size size;

        do
        {
            Console.Write("> ");
        } while (!Enum.TryParse(Console.ReadLine(), true, out size) || !Enum.IsDefined(typeof(Size), size));

        Console.WriteLine("Do you want to add an extra toppings? (y/n)");
        Console.Write("> ");

        var extraToppingsAmount = 0;

        if (Console.ReadLine()?.Trim().ToLower() == "y")
        {
            for (var i = 0; i < Pizza.ExtraToppingsCosts.Length; i++)
            {
                var prefix = i.ToString();

                if (i == Pizza.ExtraToppingsCosts.Length - 1)
                {
                    prefix += "+";
                }

                Console.WriteLine($"{prefix} - {Pizza.ExtraToppingsCosts[Int32.Min(i, Pizza.ExtraToppingsCosts.Length - 1)]:C}");
            }

            Console.WriteLine($"Pick the amount of extra toppings for your order (up to {Pizza.MaxToppings})");

            do
            {
                Console.Write("> ");
            } while (!Int32.TryParse(Console.ReadLine(), out extraToppingsAmount) || extraToppingsAmount is < 0 or > Pizza.MaxToppings);
        }

        Pizza pizza = new(size, extraToppingsAmount);

        Pizzas.Add(pizza);
    }

    private static void DisplaySummary()
    {
        decimal subTotal = 0;

        Table personalisedTable = new("Name", "Address", "Phone Number");
        personalisedTable.AddRow(Name, Address, $"+44 {PhoneNumber}");
        personalisedTable.Print();

        Table pizzaBills = new("Pizza Count", "Size", "Extra Toppings", "Total");

        for (var i = 0; i < Pizzas.Count; i++)
        {
            Pizza pizza = Pizzas[i];

            pizzaBills.AddRow(i + 1, $"{pizza.CostOfSize:C} ({pizza.Size})", $"{pizza.CostOfExtraToppings:C}", $"{pizza.Cost:C}");

            subTotal += pizza.Cost;
        }

        pizzaBills.Print();

        Table finalisedBill = new("Sub Total", "Discount", "Delivery Charge", "Grand Total");

        var discount = subTotal >= DiscountFrom ? Decimal.Round(subTotal * -DiscountRate, 2) : 0;
        var deliveryCharge = IsDelivery ? DeliveryCost : 0;

        var grandTotal = subTotal + discount + deliveryCharge;

        finalisedBill.AddRow($"{subTotal:C}", $"{discount:C}", $"{DeliveryCost:C}", $"{grandTotal:C}");

        finalisedBill.Print();

        Console.WriteLine();
    }
}
