using Common.Extensions;

namespace Algorithms.Sort;

public abstract class BaseSortAlgorithm : BaseAlgorithm
{
    private readonly List<int> _collection;

    protected int PassCounter;

    protected BaseSortAlgorithm()
    {
        const int Size = 1_000;

        Random random = new();

        _collection = [];

        for (var i = 0; i < Size; i++)
        {
            _collection.Add(random.Next(Size));
        }
    }

    protected sealed override void PrepareNextIteration()
    {
        Ints = [.._collection];
        PassCounter = 0;
    }

    protected sealed override void DisplayResult(int passes) => Console.WriteLine($"Sorted after {passes.ToSeparatedDigits()} passes");
}
