using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfObjectNode : IdentifiedNode, IHaveChildren
    {
        private readonly List<Node> _nodes;

        public ArrayOfObjectNode(uint objectId, List<Node> elements)
            : base(objectId)
        {
            _nodes = elements;
        }

        public virtual List<Node> Values
        {
            get { return _nodes; }
        }
    }
}