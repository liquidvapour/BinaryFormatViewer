using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryFormatViewer
{
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
            var fieldNames = new List<string>((int) fieldCount);

            foreach (int i in Enumerable.Range(0, Convert.ToInt32(fieldCount)))
            {
                fieldNames.Add(binaryReader.ReadString());
            }

            List<TypeSpec> typeSpecs = ReadTypeSpecs(binaryReader, fieldCount);

            var nodes = new List<Node>();
            foreach (TypeSpec typeSpec in typeSpecs)
            {
                nodes.Add(GetNodeBy(binaryReader, typeSpec, context));
            }

            var fieldNodes = new List<FieldNode>();
            foreach (int i in Enumerable.Range(0, fieldNames.Count))
            {
                fieldNodes.Add(new FieldNode(fieldNames[i], nodes[i], typeSpecs[i]));
            }

            return new RuntimeObjectNode(id, name, fieldNodes);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 4;
        }
    }
}