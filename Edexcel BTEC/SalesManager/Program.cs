using System.Globalization;

namespace SalesManager;

internal readonly record struct Employee(string Id, string Name, uint PropertiesSold);

internal static class Program
{
    private static void Main()
    {
        CultureInfo.CurrentCulture = new("en-GB");

        List<Employee> employees = [];

        uint totalPropertiesSold = 0;
        int employeeCount;

        while (true)
        {
            const int Max = 1_000;

            Console.Write($"Enter number of employees (0 - {Max}): ");
            var input = Console.ReadLine();
            Console.WriteLine();

            if (Int32.TryParse(input, out employeeCount) && employeeCount is > 0 and <= Max)
            {
                break;
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid input. Try again.");
            Console.ResetColor();
        }

        for (var i = 0; i < employeeCount; i++)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[EMPLOYEE {i + 1}/{employeeCount}]");
            Console.ResetColor();

            string name;
            while (true)
            {
                Console.Write("Enter name: ");
                var input = Console.ReadLine();
                Console.WriteLine();

                if (!String.IsNullOrWhiteSpace(input))
                {
                    name = input;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Try again.");
                Console.ResetColor();
            }

            string id;
            while (true)
            {
                Console.Write("Enter id: ");
                var input = Console.ReadLine();
                Console.WriteLine();

                if (!String.IsNullOrWhiteSpace(input))
                {
                    id = input;
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Try again.");
                Console.ResetColor();
            }

            uint propertiesSold;
            while (true)
            {
                Console.Write("Enter properties sold: ");
                var input = Console.ReadLine();
                Console.WriteLine();

                const uint Max = 100;
                if (UInt32.TryParse(input, out propertiesSold) && propertiesSold <= Max)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Try again.");
                Console.ResetColor();
            }

            totalPropertiesSold += propertiesSold;
            employees.Add(new(id, name, propertiesSold));
        }

        employees = employees.OrderByDescending(employee => employee.PropertiesSold).ToList();

        const int PropertySoldAlignment = 25;
        const int Alignment = 15;

        var divider = new string('-', 100);
        Console.WriteLine(divider);
        Console.WriteLine($"{"Id",Alignment}{"Name",Alignment}{"Properties Sold",PropertySoldAlignment}{"Sub Total",Alignment}{"Bonus",Alignment}{"Total",Alignment}");
        Console.WriteLine(divider);

        decimal totalSubTotal = 0;
        decimal totalBonus = 0;
        decimal grandTotal = 0;
        for (var i = 0; i < employees.Count; i++)
        {
            Employee employee = employees[i];

            const decimal CommissionRate = 500;
            var subTotal = employee.PropertiesSold * CommissionRate;
            totalSubTotal += subTotal;

            const decimal BonusRate = 0.15m;
            var bonus = i == 0 ? subTotal * BonusRate : 0;
            totalBonus += bonus;

            var total = subTotal + bonus;
            grandTotal += total;

            Console.WriteLine(
                $"{employee.Id,Alignment}{employee.Name,Alignment}{employee.PropertiesSold,PropertySoldAlignment}{subTotal,Alignment:C}{bonus,Alignment:C}{total,Alignment:C}"
            );
        }

        Console.WriteLine(divider);
        Console.WriteLine(
            $"{"TOTAL",Alignment}{"",Alignment}{totalPropertiesSold,PropertySoldAlignment}{totalSubTotal,Alignment:C}{totalBonus,Alignment:C}{grandTotal,Alignment:C}"
        );
    }
}
