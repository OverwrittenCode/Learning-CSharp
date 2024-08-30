namespace Algorithms.Sort;

internal sealed class InsertionSort : BaseSort
{
    protected override int ExecuteAlgorithm()
    {
        for (int i = 1; i < Ints.Count; i++)
        {
            int swapIndex = i;

            var isModified = false;

            while (swapIndex > 0 && Ints[swapIndex - 1] > Ints[swapIndex])
            {
                (Ints[swapIndex - 1], Ints[swapIndex]) = (Ints[swapIndex], Ints[swapIndex - 1]);

                swapIndex--;

                isModified = true;
            }

            if (isModified)
            {
                PassCounter++;
            }
        }

        return PassCounter;
    }
}
