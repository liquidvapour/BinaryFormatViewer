using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [System.Serializable]
    public class ArrayOfObjectNode : IdentifiedNode, IHaveChildren
    {
        private readonly IList<Node> _nodes;

        public ArrayOfObjectNode(uint objectId, IList<Node> elements)
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