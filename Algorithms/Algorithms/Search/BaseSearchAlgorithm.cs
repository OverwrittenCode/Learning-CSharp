using Common.Extensions;

namespace Algorithms.Search;

public abstract class BaseSearchAlgorithm : BaseAlgorithm
{
    private const int Size = 100_000;
    
    protected int LowerBound;
    protected int UpperBound;
    protected int RandomSearchElement;

    protected BaseSearchAlgorithm() => Ints = Enumerable.Range(0, Size).ToList();

    protected sealed override void PrepareNextIteration()
    {
        const int MaxIndex = Size - 1;
        const int ExcludedIndex = 2;
        
        LowerBound = 0;
        UpperBound = MaxIndex;
        RandomSearchElement = Ints.GetRandomElement(ExcludedIndex..^ExcludedIndex);
    }
    
    protected sealed override void DisplayResult(int index) => Console.WriteLine($"Found at index {index}");
}
