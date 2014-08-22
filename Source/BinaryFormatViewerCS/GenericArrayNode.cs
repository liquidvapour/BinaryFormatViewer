using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [Serializable]
    public class GenericArrayNode : IdentifiedNode, IHaveChildren
    {
        protected List<uint> _elementCountPerDimension;
        protected TypeSpec _typeSpec;
        protected List<Node> _vals;

        public GenericArrayNode(uint objectId, List<Node> vals, List<uint> elementCountPerDimension, TypeSpec typeSpec)
            : base(objectId)
        {
            _vals = vals;
            _elementCountPerDimension = elementCountPerDimension;
            _typeSpec = typeSpec;
        }

        public List<uint> ElementCountPerDimension
        {
            get { return _elementCountPerDimension; }
        }

        public TypeSpec TypeSpec
        {
            get { return _typeSpec; }
        }

        public virtual List<Node> Values
        {
            get { return _vals; }
        }
    }
}