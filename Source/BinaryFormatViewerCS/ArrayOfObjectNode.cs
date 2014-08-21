using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfObjectNode : IdentifiedNode, IHaveChildren
    {
        protected List<Node> _nodes;

        public virtual List<Node> Values
        {
            get
            {
                return this._nodes;
            }
        }

        public ArrayOfObjectNode(uint objectId, List<Node> elements)
            : base(objectId)
        {
            this._nodes = elements;
        }
    }
}
