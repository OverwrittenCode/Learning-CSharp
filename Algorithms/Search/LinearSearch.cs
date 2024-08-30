namespace Algorithms.Search;

internal sealed class LinearSearch : BaseSearch
{
    protected override int ExecuteAlgorithm()
    {
        for (int i = 0; i < Ints.Count; i++)
        {
            if (Ints[i] == RandomSearchElement)
            {
                return i;
            }
        }

        return -1;
    }
}
