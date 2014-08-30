using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class AssemblyPartReader : PartReader
    {
        private const int PartCode = 12;

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            return new AssemblyNode(binaryReader.ReadUInt32(), binaryReader.ReadString());
        }

        public override bool CanRead(int partCode)
        {
            return partCode == PartCode;
        }
    }
}