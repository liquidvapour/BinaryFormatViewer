using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfStringNode : IdentifiedNode, IHaveChildren
    {
        protected List<Node> _nodes;

        public ArrayOfStringNode(uint objectId, List<Node> elements)
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