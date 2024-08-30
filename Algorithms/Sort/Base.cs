using Common.Extensions;

namespace Algorithms.Sort;

internal abstract class BaseSort : BaseAlgorithm
{
    private const int Count = 100;

    private readonly List<int> _ints = [];

    public int PassCounter { get; protected set; }

    public BaseSort()
        : base(Count)
    {
        Random random = new();

        for (int i = 0; i < Count; i++)
        {
            _ints.Add(random.Next(Count));
        }

        PrepareNextIteration();
    }

    protected override void PrepareNextIteration()
    {
        Ints = new(_ints);
    }

    protected override void DisplayResult(int passes)
    {
        Console.WriteLine($"Sorted after {passes.ToSeparatedDigits()} passes");
    }
}
