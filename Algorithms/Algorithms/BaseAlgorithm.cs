using System.Diagnostics;
using Common.Extensions;

namespace Algorithms;

public abstract class BaseAlgorithm
{
    private const int Iterations = 1_000;

    protected List<int> Ints = [];

    public void Init()
    {
        long totalElapsedTicks = 0;

        var result = -1;

        for (var i = 0; i < Iterations; i++)
        {
            PrepareNextIteration();

            var stopwatch = Stopwatch.StartNew();
            result = ExecuteAlgorithm();
            stopwatch.Stop();

            totalElapsedTicks += stopwatch.Elapsed.Ticks;
        }

        Console.WriteLine($"Ticks Elapsed ({Iterations.ToSeparatedDigits()} iterations): {totalElapsedTicks.ToSeparatedDigits()}");

        DisplayResult(result);
    }

    protected abstract void PrepareNextIteration();
    protected abstract void DisplayResult(int result);
    protected abstract int ExecuteAlgorithm();
}
