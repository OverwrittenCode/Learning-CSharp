enum Shape
{
    Square,
    Circle,
    Cuboid,
} // Enum over array option index matchiing any day

enum Circle
{
    Area,
    Circumference,
}

enum Square
{
    Area,
    Perimeter,
}

enum Cuboid
{
    Volume,
    SurfaceArea,
}

internal class A
{
    private const int MaxMeasurementInput = 100;

    public static void Main()
    {
        Shape shape = GetEnumOption<Shape>();

        string result = shape switch
        {
            Shape.Square => SquareCalculation(),
            Shape.Circle => CircleCalculation(),
            Shape.Cuboid => CuboidCalculation(),
        };

        Console.WriteLine(result);
    }

    private static string CircleCalculation()
    {
        Circle option = GetEnumOption<Circle>();

        double r = GetMeasurementInput("radius");

        return option switch
        {
            Circle.Area => $"{Math.PI * r * r} squared units",
            Circle.Circumference => $"{2 * Math.PI * r} units",
        };
    }

    private static string SquareCalculation()
    {
        Square option = GetEnumOption<Square>();

        double x = GetMeasurementInput("length");

        return option switch
        {
            Square.Area => $"{x * x} squared units",
            Square.Perimeter => $"{4 * x} units",
        };
    }

    private static string CuboidCalculation()
    {
        Cuboid option = GetEnumOption<Cuboid>();

        double l = GetMeasurementInput("length");
        double w = GetMeasurementInput("width");
        double h = GetMeasurementInput("height");

        return option switch
        {
            Cuboid.Volume => $"{l * w * h} cubic units",
            Cuboid.SurfaceArea => $"{2 * ((l * w) + (w * h) + (l * h))} squared units",
        };
    }

    private static T GetEnumOption<T>()
        where T : struct, Enum
    {
        T[] options = Enum.GetValues<T>();
        foreach (T option in options)
        {
            // I would use (int)option but due to T being a generic, error "Cannot convert type 'T' to 'int'
            Console.WriteLine($"{Convert.ToInt32(option)} {option}");
        }

        Console.WriteLine("Choose an option above");

        T value;

        do
        {
            Console.WriteLine("> ");
        } while (
            !Enum.TryParse(Console.ReadLine(), true, out value) || !Enum.IsDefined(typeof(T), value)
        );

        return value;
    }

    private static double GetMeasurementInput(string name)
    {
        Console.WriteLine($"Enter a measurement for the {name} (up to {MaxMeasurementInput})");

        double value;

        do
        {
            Console.WriteLine("> ");
        } while (
            !Double.TryParse(Console.ReadLine(), out value)
            || value < 0
            || value > MaxMeasurementInput
        );

        return value;
    }
}
