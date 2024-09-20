namespace Edexcel_BTEC_L3_Computing.Unit_4.SalesManager;

internal readonly record struct Employee(string Id, string Name, int PropertiesSold)
{
    private const int ComissionRate = 500;

    public decimal Commission => Decimal.Round(PropertiesSold * ComissionRate, 2);
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
        for (int i = 0; i < rows.Length; i++)
        {
            int length =
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

        foreach (object[] rows in Rows)
        {
            int padIndex = 0;

            string value = rows.Aggregate(
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

internal sealed class SalesManager
{
    private const decimal BonusRate = 0.15M;
    private const int MaxNameLength = 20;
    private const int MaxIDLength = 20;
    private const int MaxPropertiesSold = 100;
    private const int MinEmployees = 2;
    private const int MaxEmployees = 5;

    private List<Employee> _employees = [];
    private int _totalPropertiesSold;
    private decimal _grandTotal;

    public static void Run()
    {
        new SalesManager().Main();
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

    private void Main()
    {
        while (_employees.Count < MaxEmployees)
        {
            if (_employees.Count >= MinEmployees)
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

    private void ProcessEmployee()
    {
        Console.WriteLine($"Employee {_employees.Count + 1}/{MaxEmployees}");
        Console.WriteLine(new string('-', 15));

        string name = GetEmployeeStringInput("name", MaxNameLength);
        string id = GetEmployeeStringInput("id", MaxIDLength);

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

        _employees.Add(employee);

        _totalPropertiesSold += propertiesSold;
    }

    private void DisplaySummary()
    {
        _employees = [.. _employees.OrderByDescending(employee => employee.PropertiesSold)];

        Console.WriteLine();
        Console.WriteLine("Summary");

        Table employeeTable = new("Id", "Name", "Properties Sold", "Sub Total", "Bonus", "Total");

        foreach (Employee employee in _employees)
        {
            decimal subTotal = employee.Commission;
            decimal bonus = Decimal.Round(
                subTotal * (employee == _employees[0] ? BonusRate : 0),
                2
            );

            decimal total = subTotal + bonus;
            _grandTotal += total;

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
        totalTable.AddRow($"{_totalPropertiesSold:C}", $"{_grandTotal:C}");

        totalTable.Print();
        Console.WriteLine();
    }
}
