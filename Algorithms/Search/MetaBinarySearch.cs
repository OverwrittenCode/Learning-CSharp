namespace Algorithms.Search;

internal sealed class MetaBinarySearch : BaseSearch
{
    protected override int ExecuteAlgorithm()
    {
        var index = 0;
        var maxIndex = Ints.Count - 1;

        var requiredBitsForMaxIndex = (int)Math.Floor(Math.Log2(maxIndex)) + 1;

        for (int bitPosition = requiredBitsForMaxIndex - 1; bitPosition >= 0; bitPosition--)
        {
            if (Ints[index] == RandomSearchElement)
            {
                return index;
            }

            var nextComparisonIndex = index + (1 << bitPosition);

            if (nextComparisonIndex > maxIndex)
            {
                continue;
            }

            var element = Ints[nextComparisonIndex];

            if (element == RandomSearchElement)
            {
                return nextComparisonIndex;
            }

            if (element < RandomSearchElement)
            {
                index = nextComparisonIndex;
            }
        }

        if (Ints[index] == RandomSearchElement)
        {
            return index;
        }

        return -1;
    }
}
