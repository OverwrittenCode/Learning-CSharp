namespace Algorithms.Search;

internal sealed class LinearSearch : BaseSearchAlgorithm
{
    protected override int ExecuteAlgorithm()
    {
        for (var i = 0; i < Ints.Count; i++)
        {
            if (Ints[i] == RandomSearchElement)
            {
                return i;
            }
        }

        return -1;
    }
}
