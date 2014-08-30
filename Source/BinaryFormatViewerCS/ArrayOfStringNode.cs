using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfStringNode : IdentifiedNode, IHaveChildren
    {
        private readonly IList<Node> _nodes;

        public ArrayOfStringNode(uint objectId, IList<Node> elements)
            : base(objectId)
        {
            _nodes = elements;
        }

        public IList<Node> Values
        {
            get { return _nodes; }
        }
    }
}