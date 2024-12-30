namespace Algorithms.Search;

internal sealed class ExponentialSearch : BaseSearchAlgorithm
{
    protected override int ExecuteAlgorithm()
    {
        var i = 1;

        while (i < Ints.Count && Ints[i] < RandomSearchElement)
        {
            i *= 2;
        }

        var lowerBound = i / 2;
        var upperBound = Math.Min(i, UpperBound);

        return BinarySearch.Apply(Ints, lowerBound, upperBound, RandomSearchElement);
    }
}
