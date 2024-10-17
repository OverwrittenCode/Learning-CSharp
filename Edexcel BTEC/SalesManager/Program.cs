namespace SalesManager;

internal readonly record struct Employee(string Id, string Name, int PropertiesSold)
{
    private const int CommissionRate = 500;

    public decimal Commission => Decimal.Round(PropertiesSold * CommissionRate, 2);
}

internal sealed class Table
{
    private const char CellSeparator = '|';
    private const char RowSeparator = '-';

    private readonly int[] _paddings;

    public List<object[]> Rows { get; private set; }

    public Table(params string[] headers)
    {
        Rows = [headers];
        _paddings = headers.Select(header => header.Length + 5).ToArray();
    }

    public void AddRow(params object[] rows)
    {
        for (var i = 0; i < rows.Length; i++)
        {
            var length =
                rows[i].ToString()?.Length + 5
                ?? throw new ArgumentException("ToString method returned null", nameof(rows));

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

        foreach (var rows in Rows)
        {
            var padIndex = 0;

            var value = rows.Aggregate(
                "",
                (acc, current) =>
                    acc + CellSeparator + current.ToString()?.PadRight(_paddings[padIndex++]),
                result => result + CellSeparator
            );

            Console.WriteLine(value);
            Console.WriteLine(divider);
        }
    }
}

internal sealed class Program
{
    private const decimal BonusRate = 0.15M;
    private const int MaxNameLength = 20;
    private const int MaxIDLength = 20;
    private const int MaxPropertiesSold = 100;
    private const int MinEmployees = 2;
    private const int MaxEmployees = 5;

    private static List<Employee> Employees = [];
    private static int TotalPropertiesSold;
    private static decimal GrandTotal;

    private static void Main()
    {
        while (Employees.Count < MaxEmployees)
        {
            if (Employees.Count >= MinEmployees)
            {
                Console.WriteLine("Continue? (y/n)");
                Console.Write("> ");

                if (Console.ReadLine()?.ToLower().Trim() != "y")
                {
                    break;
                }

                Console.WriteLine();
            }

            ProcessEmployee();
        }

        DisplaySummary();
    }

    private static void ProcessEmployee()
    {
        Console.WriteLine($"Employee {Employees.Count + 1}/{MaxEmployees}");
        Console.WriteLine(new string('-', 15));

        var name = GetEmployeeStringInput("name", MaxNameLength);
        var id = GetEmployeeStringInput("id", MaxIDLength);

        int propertiesSold;

        Console.WriteLine(
            $"Enter employee's number of properties sold (up to {MaxPropertiesSold})"
        );

        do
        {
            Console.Write("> ");
        } while (
            !Int32.TryParse(Console.ReadLine(), out propertiesSold)
            || propertiesSold is <= 0 or > MaxPropertiesSold
        );

        Console.WriteLine();

        Employee employee = new(id, name, propertiesSold);

        Employees.Add(employee);

        TotalPropertiesSold += propertiesSold;
    }

    private static string GetEmployeeStringInput(string field, int max)
    {
        Console.WriteLine($"Enter employee's {field} (up to {max} characters)");

        string value;

        do
        {
            Console.Write("> ");
        } while (
            Console.ReadLine() is not string input
            || (value = input).Length <= 0
            || value.Length > max
        );

        return value;
    }

    private static void DisplaySummary()
    {
        Employees = [.. Employees.OrderByDescending(employee => employee.PropertiesSold)];

        Console.WriteLine();
        Console.WriteLine("Summary");

        Table employeeTable = new("Id", "Name", "Properties Sold", "Sub Total", "Bonus", "Total");

        foreach (Employee employee in Employees)
        {
            var subTotal = employee.Commission;
            var bonus = Decimal.Round(subTotal * (employee == Employees[0] ? BonusRate : 0), 2);

            var total = subTotal + bonus;
            GrandTotal += total;

            employeeTable.AddRow(
                employee.Id,
                employee.Name,
                employee.PropertiesSold,
                $"{subTotal:C}",
                $"{bonus:C}",
                $"{total:C}"
            );
        }

        employeeTable.Print();

        Table totalTable = new("Total Properties Sold", "Total Sales Commission");
        totalTable.AddRow($"{TotalPropertiesSold:C}", $"{GrandTotal:C}");

        totalTable.Print();
        Console.WriteLine();
    }
}
