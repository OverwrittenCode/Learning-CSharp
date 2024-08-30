using System.ComponentModel;
using Algorithms;
using Algorithms.Search;
using Algorithms.Sort;
using Common.Utils;

internal class Program
{
    public static void Main()
    {
        AlgorithmType algorithmTypeChoice = ConsoleUtils.GetEnumChoice<AlgorithmType>();

        ConsoleUtils.HighlightConsoleLine(
            $"------- [{algorithmTypeChoice}] -------",
            ConsoleColor.Cyan
        );

        switch (algorithmTypeChoice)
        {
            case AlgorithmType.Search:
                {
                    SearchType searchAlgorithmChoice = ConsoleUtils.GetEnumChoice<SearchType>();

                    ConsoleUtils.HighlightConsoleLine(
                        $"----- [{searchAlgorithmChoice}] -----",
                        ConsoleColor.Cyan
                    );

                    switch (searchAlgorithmChoice)
                    {
                        case SearchType.Linear:
                            new LinearSearch().Init();

                            break;
                        case SearchType.Binary:
                            new BinarySearch().Init();

                            break;
                        case SearchType.MetaBinary:
                            new MetaBinarySearch().Init();

                            break;
                        case SearchType.Exponential:
                            new ExponentialSearch().Init();

                            break;
                        case SearchType.Interpolation:
                            new InterpolationSearch().Init();

                            break;
                        case SearchType.Ternary:
                            new TernarySearch().Init();

                            break;
                        default:
                            throw new InvalidEnumArgumentException(
                                $"Unexpected switch argument: {searchAlgorithmChoice}"
                            );
                    }
                }

                break;
            case AlgorithmType.Sort:
                {
                    SortType sortAlgorithmChoice = ConsoleUtils.GetEnumChoice<SortType>();

                    ConsoleUtils.HighlightConsoleLine(
                        $"----- [{sortAlgorithmChoice}] -----",
                        ConsoleColor.Cyan
                    );

                    switch (sortAlgorithmChoice)
                    {
                        case SortType.Bubble:
                            new BubbleSort().Init();

                            break;
                        case SortType.Insertion:
                            new InsertionSort().Init();

                            break;
                        case SortType.Merge:
                            new MergeSort().Init();

                            break;
                        default:
                            throw new InvalidEnumArgumentException(
                                $"Unexpected switch argument: {sortAlgorithmChoice}"
                            );
                    }
                }

                break;
            default:
                throw new InvalidEnumArgumentException(
                    $"Unexpected switch argument: {algorithmTypeChoice}"
                );
        }

        var startOver = ConsoleUtils.GetBooleanChoice("Would you like to start over");

        Console.WriteLine();

        if (!startOver)
        {
            ConsoleUtils.HighlightConsoleLine("Thank you for testing! Goodbye!", ConsoleColor.Cyan);

            return;
        }

        Main();
    }
}
