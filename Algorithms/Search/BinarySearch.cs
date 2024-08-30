namespace Algorithms.Search;

internal sealed class BinarySearch : BaseSearch
{
    public static int Apply(List<int> ints, int lowerBound, int upperBound, int searchElement)
    {
        while (lowerBound <= upperBound)
        {
            var midpointRange = (upperBound - lowerBound) / 2;
            var midpointIndex = lowerBound + midpointRange;
            var midpointValue = ints[midpointIndex];

            if (midpointValue == searchElement)
            {
                return midpointIndex;
            }
            else if (midpointValue < searchElement)
            {
                lowerBound = midpointIndex + 1;
            }
            else
            {
                upperBound = midpointIndex - 1;
            }
        }

        return -1;
    }

    protected override int ExecuteAlgorithm()
    {
        return Apply(Ints, LowerBound, UpperBound, RandomSearchElement);
    }
}
