using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ObjectReferencePartReader : PartReader
    {
        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            return new ObjectReferenceNode(binaryReader.ReadUInt32());
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 9;
        }
    }
}