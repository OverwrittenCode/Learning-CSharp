using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzaOrder;

internal enum SizeType
{
    Small,
    Medium,
    Large
}

internal readonly record struct Item(SizeType Size, uint ExtraToppings)
{
    private const int SizeAlignment = 20;
    private const int StandardCostAlignment = 20;
    private const int ExtraToppingsAlignment = 20;
    private const int AdditionalCostAlignment = 20;
    private const int TotalAlignment = 20;

    private static readonly string Divider = new('-', SizeAlignment + StandardCostAlignment + ExtraToppingsAlignment + AdditionalCostAlignment + TotalAlignment);

    public static void DisplayBill(Item[] items)
    {
        Console.WriteLine(
            new StringBuilder().Append($"{"Size",SizeAlignment}")
                               .Append($"{"Pizza",StandardCostAlignment}")
                               .Append($"{"Extra Toppings",ExtraToppingsAlignment}")
                               .Append($"{"Additional Cost",AdditionalCostAlignment}")
                               .Append($"{"Total",TotalAlignment}")
                               .ToString()
        );

        var subTotal = 0m;

        Console.WriteLine(Divider);
        foreach (Item item in items)
        {
            subTotal += item.Cost;

            Console.WriteLine(
                new StringBuilder().Append($"{item.Size,SizeAlignment}")
                                   .Append($"{item._standardCost,StandardCostAlignment:C}")
                                   .Append($"{item.ExtraToppings,ExtraToppingsAlignment}")
                                   .Append($"{item._additionalCost,AdditionalCostAlignment:C}")
                                   .Append($"{item.Cost,TotalAlignment:C}")
                                   .ToString()
            );
        }

        Console.WriteLine(Divider);

        const int EmptySpaceAlignment = StandardCostAlignment + ExtraToppingsAlignment + AdditionalCostAlignment;
        Console.WriteLine($"{"Sub Total",SizeAlignment}{"",EmptySpaceAlignment}{subTotal,TotalAlignment:C}");

        var discount = 0m;
        if (subTotal > 20)
        {
            discount = subTotal * 0.10m;
            Console.WriteLine($"{"Discount",SizeAlignment}{"",EmptySpaceAlignment}{-discount,TotalAlignment:C}");
        }

        const decimal DeliveryCharge = 2.50m;
        var grandTotal = subTotal - discount + DeliveryCharge;

        Console.WriteLine(
            $"""
             {"Delivery",SizeAlignment}{"",EmptySpaceAlignment}{DeliveryCharge,TotalAlignment:C}
             {"Grand Total",SizeAlignment}{"",EmptySpaceAlignment}{grandTotal,TotalAlignment:C}
             """
        );
    }

    private readonly decimal _standardCost = Size switch
    {
        SizeType.Small => 3.25m,
        SizeType.Medium => 5.50m,
        SizeType.Large => 7.15m
    };

    private readonly decimal _additionalCost = ExtraToppings switch
    {
        0 => 0m,
        1 => 0.75m,
        2 => 1.35m,
        3 => 2m,
        >= 4 => 2.50m
    };

    private decimal Cost => _standardCost + _additionalCost;
}

internal static class Program
{
    private static readonly PhoneAttribute Phone = new();
    private static readonly string SizeOptions = String.Join(", ", Enum.GetValues<SizeType>());

    private static void Main()
    {
        var name = Input("Enter your name");
        var address = Input("Enter your address");
        var phoneNumber = Input("Enter your phone number", Phone.IsValid);

        var quantity = 0;
        const int MinQuantity = 1;
        const int MaxQuantity = 6;

        Input($"Enter number of pizzas ({MinQuantity} - {MaxQuantity})", s => Int32.TryParse(s, out quantity) && quantity is >= MinQuantity and <= MaxQuantity);

        var items = new Item[quantity];
        for (var i = 0; i < quantity; i++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[PIZZA {i + 1}/{quantity}]");
            Console.ResetColor();

            SizeType size = default;
            Input($"Enter size of pizza ({SizeOptions})", s => Enum.TryParse(s, true, out size) && Enum.IsDefined(size));

            uint extraToppings = 0;
            const int MaxToppings = 50;
            Input($"Enter number of extra toppings, if any (up to {MaxToppings})", s => UInt32.TryParse(s, out extraToppings) && extraToppings <= MaxToppings);
            items[i] = new(size, extraToppings);
        }

        const int PersonalInfoAlignment = -15;
        Console.WriteLine(
            $"""
             {"Name",PersonalInfoAlignment}{name}
             {"Address",PersonalInfoAlignment}{address}
             {"Phone Number",PersonalInfoAlignment}{phoneNumber}

             """
        );

        Item.DisplayBill(items);
    }

    private static void DisplayErrorMessage()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Invalid input. Please try again.");
        Console.ResetColor();
    }

    private static string Input(string prompt, Func<string, bool>? postValidation = null)
    {
        prompt += ": ";

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(prompt);
            Console.ResetColor();

            if (Console.ReadLine()?.Trim() is { Length: > 0 } input && (postValidation is null || postValidation(input)))
            {
                return input;
            }

            DisplayErrorMessage();
        }
    }
}
