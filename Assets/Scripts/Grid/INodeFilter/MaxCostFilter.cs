using System.Collections.Generic;

public class MaxCostFilter : INodeFilter
{

    public static MaxCostFilter Filter = new MaxCostFilter();
    public List<Node> Filtrate(List<Node> nodes)
    {
        var maxCost = 0;
        var result = new List<Node>();

        foreach (var node in nodes)
        {
            if (node.Cost > maxCost)
            {
                maxCost = node.Cost;
                result.Clear();
            }
            if (node.Cost == maxCost)
                result.Add(node);
        }

        return result;
    }
}
