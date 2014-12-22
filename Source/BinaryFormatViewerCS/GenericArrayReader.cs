using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryFormatViewer
{
    public class GenericArrayReader : ObjectReaderBase
    {
        public GenericArrayReader(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
            : base(partProvider, primitiveTypeReader)
        {
        }

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            uint objectId = binaryReader.ReadUInt32();
            binaryReader.ReadByte();
            uint numberOfDimentions = binaryReader.ReadUInt32();

            var elementCountPerDimention = Enumerable.Range(0, (int) numberOfDimentions).Select(i => binaryReader.ReadUInt32()).ToList();

            List<TypeSpec> typeSpecs = ReadTypeSpecs(binaryReader, 1U);

            var nodes = new List<Node>();

            int nullItemsLeft = 0;

            foreach (var i in Enumerable.Range(0, GetTotalElementCount(elementCountPerDimention)))
            {
                Node node = null;
                if (nullItemsLeft == 0)
                {
                    node = GetNodeBy(binaryReader, typeSpecs[0], context);
                    var arrayFilterNode = node as ArrayFilterNode;
                    if (arrayFilterNode != null)
                    {
                        nullItemsLeft = (int) arrayFilterNode.NumberOfNullItems;
                    }
                }
                if (nullItemsLeft > 0)
                {
                    node = new NullNode();
                    checked
                    {
                        --nullItemsLeft;
                    }
                }
                nodes.Add(node);
            }

            return new GenericArrayNode(objectId, nodes, elementCountPerDimention, typeSpecs[0]);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 7;
        }

        private int GetTotalElementCount(IEnumerable<uint> elementCountPerDimention)
        {
            long result = elementCountPerDimention.Sum(x => (long)x);
            return checked((int)result);
        }
    }
}