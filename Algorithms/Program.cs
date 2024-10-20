using Algorithms;
using Algorithms.Search;
using Algorithms.Sort;
using Common.Utils;

AlgorithmType algorithmTypeChoice = ConsoleUtils.GetEnumChoice<AlgorithmType>();

ConsoleUtils.HighlightConsoleLine($"------- [{algorithmTypeChoice}] -------", ConsoleColor.Cyan);

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
            }
        }

        break;
}
