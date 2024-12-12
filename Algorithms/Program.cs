using Algorithms.Enums;
using Algorithms.Search;
using Algorithms.Search.Enums;
using Algorithms.Sort;
using Algorithms.Sort.Enums;
using Common.Utils;

var algorithmTypeChoice = ConsoleUtils.GetEnumChoice<AlgorithmType>();

ConsoleUtils.HighlightConsoleLine($"------- [{algorithmTypeChoice}] -------", ConsoleColor.Cyan);

switch (algorithmTypeChoice)
{
    case AlgorithmType.Search:
        {
            var searchAlgorithmChoice = ConsoleUtils.GetEnumChoice<SearchType>();

            ConsoleUtils.HighlightConsoleLine($"----- [{searchAlgorithmChoice}] -----", ConsoleColor.Cyan);

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
            var sortAlgorithmChoice = ConsoleUtils.GetEnumChoice<SortType>();

            ConsoleUtils.HighlightConsoleLine($"----- [{sortAlgorithmChoice}] -----", ConsoleColor.Cyan);

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
