using System;
using System.Collections.Generic;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    
    public class RefTypeObjectPartReader : ObjectReaderBase
    {
        public RefTypeObjectPartReader(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
            : base(partProvider, primitiveTypeReader)
        {
        }

        //TODO: rewrite to foreach.
        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            uint id = binaryReader.ReadUInt32();
            uint index1 = binaryReader.ReadUInt32();
            IHaveTypeSpecs haveTypeSpecs = (IHaveTypeSpecs)context.ExistingObjects[index1];
            int num1 = 0;
            AssemblyRefNode assemblyRefNode = haveTypeSpecs.Assembly as AssemblyRefNode;
            if (assemblyRefNode != null)
            {
                num1 = (int)assemblyRefNode.Id;
            }
            else
            {
                AssemblyNode assemblyNode = haveTypeSpecs.Assembly as AssemblyNode;
                if (assemblyNode != null)
                    num1 = (int)assemblyNode.Id;
            }
            Dictionary<string, Node> dictionary = new Dictionary<string, Node>();
            int num2 = 0;
            int count = haveTypeSpecs.Fields.Count;
            if (count < 0)
                throw new ArgumentOutOfRangeException("max");
            while (num2 < count)
            {
                int index2 = num2;
                ++num2;
                dictionary.Add(haveTypeSpecs.Fields[index2].Name, this.GetNodeBy(binaryReader, haveTypeSpecs.Fields[index2].TypeSpec, context));
            }
            return (Node)new ObjectNode(id, haveTypeSpecs.Name, checked((uint)num1), haveTypeSpecs.Fields);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 1;
        }
    }
}
