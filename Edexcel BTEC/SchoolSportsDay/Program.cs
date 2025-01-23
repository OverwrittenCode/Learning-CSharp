namespace SchoolSportsDay;

internal static class Program
{
    private static void Main()
    {
        var redCounter = 0;
        var greenCounter = 0;
        var whiteCounter = 0;

        const int StudentCount = 10;
        for (var i = 0; i < StudentCount; i++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[STUDENT {i + 1}/{StudentCount}]");

            int birthMonth;
            while (true)
            {
                Console.Write("Enter student's birth month: ");
                if (Int32.TryParse(Console.ReadLine(), out birthMonth) && birthMonth is >= 1 and <= 12)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please try again.");
                Console.ResetColor();
            }

            switch (birthMonth)
            {
                case <= 4:
                    redCounter++;
                    break;
                case <= 8:
                    greenCounter++;
                    break;
                default:
                    whiteCounter++;
                    break;
            }

            const int Alignment = 10;
            var divider = new string('-', Alignment);

            Console.WriteLine(
                $"""
                 {"Group",Alignment}{"Amount",Alignment}
                 {divider}
                 {"Red",Alignment}{redCounter,Alignment}
                 {"Green",Alignment}{greenCounter,Alignment}
                 {"White",Alignment}{whiteCounter,Alignment}
                 """
            );
        }
    }
}
