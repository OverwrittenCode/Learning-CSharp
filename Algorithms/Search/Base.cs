using Common.Extensions;

namespace Algorithms.Search;

internal abstract class BaseSearch : BaseAlgorithm
{
    private const int Count = 100_000;
    private const int ExcludedIndex = 2;

    public readonly int RandomSearchElement;

    public BaseSearch() : base(Count)
    {
        Ints = Enumerable.Range(0, Count).ToList();

        RandomSearchElement = Ints.GetRandomElement(ExcludedIndex..^ExcludedIndex);
        RandomSearchElement = Ints.Count - 1;
    }

    protected override void DisplayResult(int index) => Console.WriteLine($"Found at index {index}");
}
