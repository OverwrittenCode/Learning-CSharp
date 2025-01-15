namespace MovieTickets;

internal enum GroupType
{
    Child,
    Adult,
    Senior
}

internal sealed class Program
{
    private static readonly Dictionary<GroupType, decimal> TicketPrices2 = new()
    {
        {
            GroupType.Child, 5
        },
        {
            GroupType.Adult, 10
        },
        {
            GroupType.Senior, 7
        }
    };

    private static void Main()
    {
        decimal price = 0;
        uint totalTickets = 0;

        foreach ((GroupType group, var ticketPrice) in TicketPrices2)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[{group} TICKETS: {ticketPrice}/ticket]");
            Console.ResetColor();

            const int Max = 30;

            uint standardTickets;
            while (true)
            {
                Console.Write($"Enter quantity for standard tickets (up to {Max}): ");
                if (UInt32.TryParse(Console.ReadLine(), out standardTickets) && standardTickets <= Max)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ResetColor();
            }

            uint premiumTickets;
            while (true)
            {
                Console.Write($"Enter quantity for premium tickets (up to {Max}): ");
                if (UInt32.TryParse(Console.ReadLine(), out premiumTickets) && premiumTickets <= Max)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ResetColor();
            }

            const decimal PremiumTicketCharge = 2;

            var quantity = standardTickets + premiumTickets;
            var standardCost = ticketPrice * quantity;
            var additionalCost = premiumTickets * PremiumTicketCharge;

            price += standardCost + additionalCost;
            totalTickets += quantity;
        }

        if (totalTickets > 5)
        {
            price *= 0.9m;
        }

        Console.WriteLine($"Total cost: {price:C}");
    }
}
