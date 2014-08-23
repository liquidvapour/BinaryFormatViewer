using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryFormatViewer
{
    public static class NodeExtentions
    {
        public static IEnumerable<IdentifiedNode> GetIdentifiedNodes(this IEnumerable<Node> nodes)
        {
            return nodes.OfType<IdentifiedNode>();
        }
    }
}