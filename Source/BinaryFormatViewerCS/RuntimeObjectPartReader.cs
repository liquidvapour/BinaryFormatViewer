using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryFormatViewer
{
    [Serializable]
    public class RuntimeObjectPartReader : ObjectReaderBase
    {
        public RuntimeObjectPartReader(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
            : base(partProvider, primitiveTypeReader)
        {
        }

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            uint id = binaryReader.ReadUInt32();
            string name = binaryReader.ReadString();

            uint fieldCount = binaryReader.ReadUInt32();
            List<string> fieldNames = new List<string>((int)fieldCount);

            foreach (var i in Enumerable.Range(0, Convert.ToInt32(fieldCount)))
            {
                fieldNames.Add(binaryReader.ReadString());
            }

            List<TypeSpec> typeSpecs = this.ReadTypeSpecs(binaryReader, fieldCount);

            List<Node> nodes = new List<Node>();
            foreach (var typeSpec in typeSpecs)
            {
                nodes.Add(this.GetNodeBy(binaryReader, typeSpec, context));
            }

            List<FieldNode> fieldNodes = new List<FieldNode>();
            foreach (var i in Enumerable.Range(0, fieldNames.Count))
            {
                fieldNodes.Add(new FieldNode(fieldNames[i], nodes[i], typeSpecs[i]));
            }

            return (Node)new RuntimeObjectNode(id, name, fieldNodes);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 4;
        }
    }
}
