namespace ShapeCalculations;

internal enum Panel
{
    Quit,
    Help,
    Calculate
}

internal enum Shape
{
    Circle,
    Square,
    Cuboid
}

internal enum Circle
{
    Area,
    Circumference
}

internal enum Square
{
    Area,
    Perimeter
}

internal enum Cuboid
{
    Volume,
    SA
}

internal static class Program
{
    private static void Main()
    {
        var calculationCounter = 1;

        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[CALCULATION {calculationCounter}]");
            Console.ResetColor();

            var panel = GetEnumChoice<Panel>();

            switch (panel)
            {
                case Panel.Quit:
                    QuitProgram();
                    return;
                case Panel.Help:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[HELP MENU]");
                    Console.ResetColor();
                    Console.WriteLine(
                        """
                        You can perform calculations on a Circle, Square, or Cuboid.
                        Each shape allows for different types of calculations (e.g. Area, Perimeter).
                        """
                    );
                    break;
                case Panel.Calculate:
                    calculationCounter++;
                    Calculate();
                    break;
            }

            Console.WriteLine();
        }
    }

    private static void QuitProgram()
    {
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
        Environment.Exit(0);
    }

    private static void Calculate()
    {
        string result;
        switch (GetEnumChoice<Shape>())
        {
            case Shape.Circle:
                {
                    var attribute = GetEnumChoice<Circle>();
                    var radius = GetMeasurementInput("radius");

                    result = attribute switch
                    {
                        Circle.Area => $"{Math.PI * radius * radius} squared units",
                        Circle.Circumference => $"{2 * Math.PI * radius} units"
                    };
                }
                break;

            case Shape.Square:
                {
                    var attribute = GetEnumChoice<Square>();
                    var x = GetMeasurementInput("length");

                    result = attribute switch
                    {
                        Square.Area => $"{x * x} squared units",
                        Square.Perimeter => $"{4 * x} units"
                    };
                }
                break;

            case Shape.Cuboid:
                {
                    var attribute = GetEnumChoice<Cuboid>();

                    var l = GetMeasurementInput("length");
                    var w = GetMeasurementInput("width");
                    var h = GetMeasurementInput("height");

                    result = attribute switch
                    {
                        Cuboid.Volume => $"{l * w * h} cubic units",
                        Cuboid.SA => $"{2 * ((l * w) + (l * h) + (w * h))} squared units"
                    };
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        Console.WriteLine($"Result: {result}");
    }

    private static double GetMeasurementInput(string name)
    {
        while (true)
        {
            const int Max = 100;

            if (Double.TryParse(Input($"Enter a measurement number for {name} (0 - {Max})"), out var input) && input is > 0 and < Max)
            {
                return input;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please try again.");
            Console.ResetColor();
        }
    }

    private static T GetEnumChoice<T>() where T : struct, Enum
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(String.Join(", ", Enum.GetNames<T>()));
        Console.ResetColor();

        while (true)
        {
            if (Enum.TryParse(Input("Enter an option from above"), true, out T choice) && Enum.IsDefined(choice))
            {
                Console.WriteLine();
                return choice;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Please try again.");
            Console.ResetColor();
        }
    }

    private static string? Input(string prompt)
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Write($"{prompt}: ");
        Console.ResetColor();
        return Console.ReadLine();
    }
}
