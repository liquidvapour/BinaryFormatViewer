using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class StringPartReader : PartReader
    {
        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            return (Node)new StringNode(binaryReader.ReadUInt32(), binaryReader.ReadString());
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 6;
        }
    }
}
