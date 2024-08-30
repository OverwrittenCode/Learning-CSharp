namespace Algorithms.Search;

internal sealed class TernarySearch : BaseSearch
{
    protected override int ExecuteAlgorithm()
    {
        while (LowerBound <= UpperBound)
        {
            var rangeThird = (UpperBound - LowerBound) / 3;

            var lowerThird = LowerBound + rangeThird;
            var upperThird = UpperBound - rangeThird;

            if (Ints[lowerThird] == RandomSearchElement)
            {
                return lowerThird;
            }

            if (Ints[upperThird] == RandomSearchElement)
            {
                return upperThird;
            }

            if (RandomSearchElement < Ints[lowerThird])
            {
                UpperBound = lowerThird - 1;
            }
            else if (RandomSearchElement > Ints[upperThird])
            {
                LowerBound = upperThird + 1;
            }
            else
            {
                LowerBound = lowerThird + 1;
                UpperBound = upperThird - 1;
            }
        }

        return -1;
    }
}
