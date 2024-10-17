using System.Text.RegularExpressions;

namespace PasswordRating;

internal sealed partial class Program
{
    private const int MaxPasswordInput = 100;
    private const int MinPasswordLength = 8;
    private const int MaxPasswordLength = 15;
    private const int LowerCaseFactor = 5;
    private const int UpperCaseFactor = 5;
    private const int NumericalFactor = 10;
    private const int SpecialFactor = 10;
    private const int PadMaxWidth = 40;
    private const int PadRightWidth = 25;
    private const int PadLeftWidth = PadMaxWidth - PadRightWidth - 1;

    private string _password = "";
    private int _score;
    private int _reductionScore;
    private int _lowerCaseCounter;
    private int _upperCaseCounter;
    private int _numericCounter;
    private int _specialCounter;

    private int Length => _password.Length;

    private static void Main()
    {
        Console.WriteLine("Password Rating Service");
        Console.WriteLine(new string('=', PadMaxWidth));

        Program passwordRating = new();
        passwordRating.Process();
    }

    [GeneratedRegex("[a-z]")]
    private static partial Regex LowercaseRegex();

    [GeneratedRegex("[A-Z]")]
    private static partial Regex UppercaseRegex();

    [GeneratedRegex("\\d")]
    private static partial Regex NumericalRegex();

    [GeneratedRegex("!|%|&|\\*|\\+|=")]
    private static partial Regex SpecialRegex();

    private static void PrintRow(string description, string value) =>
        Console.WriteLine($"{description, -PadRightWidth} {value, PadLeftWidth}");

    private void Process()
    {
        Console.WriteLine(
            $"Enter password (advised {MinPasswordLength} - {MaxPasswordLength}, maximum {MaxPasswordInput} characters)"
        );

        while (true)
        {
            Console.Write("> ");

            var input = Console.ReadLine();

            if (input is not null && input.Length > 0 && input.Length < MaxPasswordInput)
            {
                _password = input;
                break;
            }
        }

        Console.WriteLine();

        foreach (char character in _password.ToCharArray())
        {
            var regexInput = character.ToString();
            if (LowercaseRegex().IsMatch(regexInput))
            {
                _score += LowerCaseFactor;
                _lowerCaseCounter++;
            }
            else if (UppercaseRegex().IsMatch(regexInput))
            {
                _score += UpperCaseFactor;
                _upperCaseCounter++;
            }
            else if (NumericalRegex().IsMatch(regexInput))
            {
                _score += NumericalFactor;
                _numericCounter++;
            }
            else if (SpecialRegex().IsMatch(regexInput))
            {
                _score += SpecialFactor;
                _specialCounter++;
            }
        }

        if (_lowerCaseCounter == Length)
        {
            _reductionScore = _lowerCaseCounter * 3;
        }
        else if (_upperCaseCounter == Length)
        {
            _reductionScore = _upperCaseCounter * 3;
        }
        else if (_numericCounter == Length)
        {
            _reductionScore = _numericCounter * 5;
        }

        DisplaySummary();
    }

    private void DisplaySummary()
    {
        if (Length > MaxPasswordLength)
        {
            Console.WriteLine($"Your password is too long (>{MaxPasswordLength} characters)");
        }
        else if (Length < MinPasswordLength)
        {
            Console.WriteLine($"Your password is too short (<{MinPasswordLength} characters)");
        }

        var securityRating = _score switch
        {
            <= 20 => "Very Low",
            <= 40 => "Low",
            <= 70 => "Medium",
            <= 90 => "High",
            _ => "Very High",
        };

        Console.WriteLine();
        Console.WriteLine($"Security Rating: {securityRating}");

        var lowerCaseScore = _lowerCaseCounter * LowerCaseFactor;
        var upperCaseScore = _upperCaseCounter * UpperCaseFactor;
        var numericalScore = _numericCounter * NumericalFactor;
        var specialScore = _specialCounter * SpecialFactor;

        Console.WriteLine(new string('-', PadMaxWidth));
        PrintRow("Description", "Score");
        Console.WriteLine(new string('-', PadMaxWidth));

        PrintRow("Lower Case", lowerCaseScore.ToString());
        PrintRow("Upper Case", upperCaseScore.ToString());
        PrintRow("Digits", numericalScore.ToString());
        PrintRow("Special", specialScore.ToString());

        if (Length >= 10)
        {
            _score += 20;
            PrintRow("Bonus (10+ characters)", "20");
        }

        if (_reductionScore != 0)
        {
            _score -= _reductionScore;
            PrintRow("Limited Character Types", (-_reductionScore).ToString());
        }

        Console.WriteLine(new string('-', PadMaxWidth));
        PrintRow("Total", _score.ToString());
        Console.WriteLine(new string('-', PadMaxWidth));

        Console.WriteLine("Thank you for using our Password Rating Service!");
        Console.WriteLine();
    }
}
