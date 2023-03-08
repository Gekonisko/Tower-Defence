using System.Collections.Generic;

public interface INodeFilter
{ 
    public List<Node> Filtrate(List<Node> nodes);
}
