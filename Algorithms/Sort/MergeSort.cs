namespace Algorithms.Sort;

internal sealed class MergeSort : BaseSort
{
    private static void Merge(List<int> left, List<int> right, List<int> ints)
    {
        var length = ints.Count;

        var leftLength = ints.Count / 2;
        var rightLength = length - leftLength;

        var leftIndex = 0;
        var rightIndex = 0;
        var i = 0;

        while (leftIndex < leftLength && rightIndex < rightLength)
        {
            var index = i++;

            if (left[leftIndex] < right[rightIndex])
            {
                ints[index] = left[leftIndex++];
            }
            else
            {
                ints[index] = right[rightIndex++];
            }
        }

        while (leftIndex < leftLength)
        {
            ints[i++] = left[leftIndex++];
        }

        while (rightIndex < rightLength)
        {
            ints[i++] = right[rightIndex++];
        }
    }

    protected override int ExecuteAlgorithm()
    {
        Apply(Ints);

        return PassCounter;
    }

    private void Apply(List<int> ints)
    {
        var length = ints.Count;

        if (length < 2)
        {
            return;
        }

        var lengthHalf = length / 2;

        List<int>? left = ints[..^lengthHalf];
        List<int>? right = ints[lengthHalf..];

        Apply(left);
        Apply(right);

        PassCounter++;

        Merge(left, right, ints);
    }
}
