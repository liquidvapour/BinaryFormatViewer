using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [Serializable]
    public class GenericArrayNode : IdentifiedNode, IHaveChildren
    {
        protected List<Node> _vals;
        protected List<uint> _elementCountPerDimension;
        protected TypeSpec _typeSpec;

        public virtual List<Node> Values
        {
            get
            {
                return this._vals;
            }
        }

        public List<uint> ElementCountPerDimension
        {
            get
            {
                return this._elementCountPerDimension;
            }
        }

        public TypeSpec TypeSpec
        {
            get
            {
                return this._typeSpec;
            }
        }

        public GenericArrayNode(uint objectId, List<Node> vals, List<uint> elementCountPerDimension, TypeSpec typeSpec)
            : base(objectId)
        {
            this._vals = vals;
            this._elementCountPerDimension = elementCountPerDimension;
            this._typeSpec = typeSpec;
        }
    }
}
