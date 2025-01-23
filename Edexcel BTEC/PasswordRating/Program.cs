string password;

while (true)
{
    const int Min = 8;
    const int Max = 15;

    Console.Write($"Enter Password ({Min} - {Max} characters): ");
    var input = Console.ReadLine();

    if (!String.IsNullOrEmpty(input) && input.Length is >= Min and <= Max)
    {
        password = input;
        break;
    }

    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Invalid input. Please try again.");
    Console.ResetColor();
}

var passwordLength = password.Length;

var lower = 0;
var upper = 0;
var numeric = 0;
var special = 0;
var length = 0;
var penalty = 0;

if (passwordLength >= 10)
{
    length = 10;
}

if (password.All(c => c is >= 'A' and <= 'Z') || password.All(c => c is >= 'a' and <= 'z'))
{
    penalty = passwordLength * 3;
}
else if (password.All(c => c is >= '0' and <= '9'))
{
    penalty = passwordLength * 5;
}

foreach (var character in password)
{
    switch (character)
    {
        case >= 'a' and <= 'z':
            lower += 5;
            break;
        case >= 'A' and <= 'Z':
            upper += 5;
            break;
        case >= '0' and <= '9':
            numeric += 10;
            break;
        case '!' or '%' or '&' or '*' or '+' or '=':
            special += 10;
            break;
    }
}

var score = lower + upper + numeric + special - penalty;

var rating = score switch
{
    <= 20 => "Very low",
    <= 40 => "Low",
    <= 70 => "Medium",
    <= 80 => "High",
    _ => "Very high"
};

const int TypeAlignment = 20;
const int PointAlignment = 10;

var divider = new string('-', TypeAlignment + PointAlignment);

Console.WriteLine(
    $"""
     {divider}
     {"Type",TypeAlignment}{"Points",PointAlignment}
     {divider}
     {"Lower case",TypeAlignment}{lower,PointAlignment}
     {"Upper case",TypeAlignment}{upper,PointAlignment}
     {"Digits",TypeAlignment}{numeric,PointAlignment}
     {"Special character",TypeAlignment}{special,PointAlignment}
     """
);

if (length > 0)
{
    Console.WriteLine($"{"10+ Characters",TypeAlignment}{length,PointAlignment}");
}

if (penalty > 0)
{
    Console.WriteLine($"{"Lack of variety",TypeAlignment}{-penalty,PointAlignment}");
}

Console.WriteLine(
    $"""
     {divider}
     {"Score",TypeAlignment}{score,PointAlignment}
     {"Rating",TypeAlignment}{rating,PointAlignment}
     """
);
