namespace ShapeCalculations;

internal enum Panel
{
    Quit,
    Help,
    Calculate
}

internal enum PostCalculation
{
    Restart,
    QuitProgram
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
    SurfaceArea
}

internal sealed class Program
{
    private const int MaxMeasurementInput = 100;

    private static void Main()
    {
        var calculationCounter = 1;

        while (true)
        {
            Console.WriteLine($"----- Calculation {calculationCounter} -----");

            var panel = GetEnumChoice<Panel>();

            switch (panel)
            {
                case Panel.Quit:
                    QuitProgram();
                    break;
                case Panel.Help:
                    DisplayHelpMenu();
                    break;
                case Panel.Calculate:
                    calculationCounter++;
                    PerformCalculations();
                    break;
            }
        }
    }

    private static void QuitProgram()
    {
        Console.WriteLine("Thank you for using this program! Goodbye!");
        Environment.Exit(0);
    }

    private static void DisplayHelpMenu()
    {
        Console.WriteLine("----- [Help Menu] -----");
        Console.WriteLine("You can perform calculations on a Circle, Square, or Cuboid.");
        Console.WriteLine("Each shape allows for different types of calculations (e.g. Area, Perimeter).");
    }

    private static void PerformCalculations()
    {
        var shape = GetEnumChoice<Shape>();

        var result = shape switch
        {
            Shape.Circle => GetCircleCalculation(),
            Shape.Square => GetSquareCalculation(),
            Shape.Cuboid => GetCuboidCalculation()
        };

        Console.WriteLine($"Result: {result}");
        Console.WriteLine();

        var postCalculation = GetEnumChoice<PostCalculation>();

        if (postCalculation == PostCalculation.QuitProgram)
        {
            QuitProgram();
        }
    }

    private static string GetCircleCalculation()
    {
        var circle = GetEnumChoice<Circle>();
        var radius = GetMeasurementInput("radius");

        return circle switch
        {
            Circle.Area => $"{Math.PI * radius * radius} squared units",
            Circle.Circumference => $"{2 * Math.PI * radius} units"
        };
    }

    private static string GetSquareCalculation()
    {
        var square = GetEnumChoice<Square>();
        var length = GetMeasurementInput("length");

        return square switch
        {
            Square.Area => $"{length * length} squared units",
            Square.Perimeter => $"{4 * length} units"
        };
    }

    private static string GetCuboidCalculation()
    {
        var cuboid = GetEnumChoice<Cuboid>();
        var length = GetMeasurementInput("length");
        var width = GetMeasurementInput("width");
        var height = GetMeasurementInput("height");

        return cuboid switch
        {
            Cuboid.SurfaceArea => $"{2 * ((length * width) + (length * height) + (width * height))} squared units",
            Cuboid.Volume => $"{length * width * height} cubic units"
        };
    }

    private static double GetMeasurementInput(string name)
    {
        Console.WriteLine($"Enter a positive number for the {name} (up to {MaxMeasurementInput})");

        double input;

        do
        {
            Console.Write("> ");
        } while (!Double.TryParse(Console.ReadLine(), out input) || input <= 0 || input > MaxMeasurementInput);

        Console.WriteLine();

        return input;
    }

    private static T GetEnumChoice<T>() where T : struct, Enum
    {
        Console.WriteLine("Choose an option below:");

        foreach (T option in Enum.GetValues<T>())
        {
            Console.WriteLine($"{Convert.ToInt32(option)} - {option}");
        }

        T choice;

        do
        {
            Console.Write("> ");
        } while (!Enum.TryParse(Console.ReadLine(), true, out choice) || !Enum.IsDefined(typeof(T), choice));

        Console.WriteLine();

        return choice;
    }
}
