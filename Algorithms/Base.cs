using System.Diagnostics;
using Common.Extensions;

namespace Algorithms;

internal abstract class BaseAlgorithm
{
    public const int Iterations = 1_000;

    public List<int> Ints { get; protected set; }
    public int LowerBound { get; protected set; }
    public int UpperBound { get; protected set; }

    public BaseAlgorithm(int maxLength)
    {
        UpperBound = maxLength - 1;

        Ints = new(UpperBound);
    }

    public void Init()
    {
        long totalElapsedTicks = 0;

        var result = -1;

        for (var i = 0; i < Iterations; i++)
        {
            var stopwatch = Stopwatch.StartNew();

            result = ExecuteAlgorithm();

            stopwatch.Stop();

            totalElapsedTicks += stopwatch.Elapsed.Ticks;

            PrepareNextIteration();
        }

        var heading = $"Ticks Elapsed ({Iterations.ToSeparatedDigits()} iterations)";
        var body = totalElapsedTicks.ToSeparatedDigits();

        Console.WriteLine($"{heading}: {body}");

        DisplayResult(result);

        Console.WriteLine();
    }

    protected abstract int ExecuteAlgorithm();

    protected virtual void PrepareNextIteration() { }

    protected virtual void DisplayResult(int result) { }
}
