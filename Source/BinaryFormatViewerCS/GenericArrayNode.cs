using System.Collections.Generic;

namespace BinaryFormatViewer
{
    public class GenericArrayNode : IdentifiedNode, IHaveChildren
    {
        public GenericArrayNode(uint objectId, IList<Node> vals, List<uint> elementCountPerDimension, TypeSpec typeSpec)
            : base(objectId)
        {
            Values = vals;
            ElementCountPerDimension = elementCountPerDimension;
            TypeSpec = typeSpec;
        }

        public List<uint> ElementCountPerDimension { get; private set; }

        public TypeSpec TypeSpec { get; private set; }

        public IList<Node> Values { get; private set; }
    }
}