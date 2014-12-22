using System.Collections.Generic;

namespace BinaryFormatViewer
{
    public class ArrayOfObjectNode : IdentifiedNode, IHaveChildren
    {
        private readonly IList<Node> _nodes;

        public ArrayOfObjectNode(uint objectId, IList<Node> elements)
            : base(objectId)
        {
            _nodes = elements;
        }

        public IList<Node> Children
        {
            get { return _nodes; }
        }
    }
}