using System.Text;

namespace BreadthFirstSearch;

internal sealed class NodeTree
{
    public int Value { get; init; }
    public NodeTree? Left { get; init; }
    public NodeTree? Right { get; init; }
}

internal static class Program
{
    private static readonly NodeTree NodeTree = new()
    {
        Value = 1,
        Left = new()
        {
            Value = 2,
            Left = new()
            {
                Value = 4
            },
            Right = new()
            {
                Value = 5
            }
        },
        Right = new()
        {
            Value = 3,
            Left = new()
            {
                Value = 6
            }
        }
    };

    private static void Main()
    {
        Queue<(NodeTree Node, int Depth)> queue = new();

        queue.Enqueue((NodeTree, 0));

        var lastDepth = 0;
        var output = new StringBuilder();

        while (queue.Count > 0)
        {
            var (node, depth) = queue.Dequeue();

            if (depth > lastDepth)
            {
                output.AppendLine();
                lastDepth = depth;
            }

            output.Append($"{node.Value} ");

            if (node.Left != null)
            {
                queue.Enqueue((node.Left, depth + 1));
            }

            if (node.Right != null)
            {
                queue.Enqueue((node.Right, depth + 1));
            }
        }

        Console.WriteLine(output.ToString());
    }
}
