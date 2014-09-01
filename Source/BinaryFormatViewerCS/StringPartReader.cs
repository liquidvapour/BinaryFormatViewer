using System.IO;

namespace BinaryFormatViewer
{
    public class StringPartReader : PartReader
    {
        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            return new StringNode(binaryReader.ReadUInt32(), binaryReader.ReadString());
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 6;
        }
    }
}