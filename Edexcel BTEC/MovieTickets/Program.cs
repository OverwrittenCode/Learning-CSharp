namespace MovieTickets;

internal enum GroupType
{
    Child,
    Adult,
    Senior
}

internal sealed class Program
{
    private static readonly GroupType[] Groups = Enum.GetValues<GroupType>();
    private static readonly int[] TicketPrices = new int[Groups.Length];

    private static void Main()
    {
        Console.WriteLine($"Do you want premium seats ({2:C}/ticket)? (y/n)");
        var isPremiumSeats = Console.ReadLine()?.ToLower().Trim() == "y";

        decimal price = 0;
        decimal totalTickets = 0;

        foreach (GroupType group in Groups)
        {
            int ticketCount;

            Console.WriteLine($"How many {group} tickets do you want to buy? (0 - 30)");

            do
            {
                Console.Write("> ");
            } while (!Int32.TryParse(Console.ReadLine(), out ticketCount) || ticketCount is < 0 or > 30);

            totalTickets += ticketCount;
            price += TicketPrices[(int)group] * ticketCount;

            if (isPremiumSeats)
            {
                price += 2;
            }
        }

        if (totalTickets > 5)
        {
            price *= 0.9M;
        }

        Console.WriteLine($"Total cost: {price:C}");
    }

    static Program()
    {
        TicketPrices[(int)GroupType.Child] = 5;
        TicketPrices[(int)GroupType.Adult] = 10;
        TicketPrices[(int)GroupType.Senior] = 7;
    }
}
