namespace Algorithms.Search;

sealed class InterpolationSearch : BaseSearch
{
    protected override int ExecuteAlgorithm()
    {
        while (
            LowerBound <= UpperBound
            && RandomSearchElement >= Ints[LowerBound]
            && RandomSearchElement <= Ints[UpperBound]
        )
        {
            var index =
                LowerBound
                + (
                    (UpperBound - LowerBound)
                    / (Ints[UpperBound] - Ints[LowerBound])
                    * (RandomSearchElement - Ints[LowerBound])
                );

            var element = Ints[index];

            if (element == RandomSearchElement)
            {
                return index;
            }
            else if (element < RandomSearchElement)
            {
                LowerBound = index + 1;
            }
            else
            {
                UpperBound = index - 1;
            }
        }

        return -1;
    }
}
