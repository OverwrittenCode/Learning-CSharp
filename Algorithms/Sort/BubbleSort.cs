namespace Algorithms.Sort;

internal sealed class BubbleSort : BaseSort
{
    protected override int ExecuteAlgorithm()
    {
        var isModified = true;

        while (isModified)
        {
            isModified = false;
            PassCounter++;

            for (var i = 0; i < Ints.Count - 1; i++)
            {
                if (Ints[i] > Ints[i + 1])
                {
                    (Ints[i], Ints[i + 1]) = (Ints[i + 1], Ints[i]);

                    isModified = true;
                }
            }
        }

        return PassCounter;
    }
}
