using System.Collections.Generic;

namespace Hg;

public class NodeList : List<Node>
{
    public static NodeList operator +(NodeList left, NodeList right)
    {
        left.AddRange(right);
        return left;
    }
}