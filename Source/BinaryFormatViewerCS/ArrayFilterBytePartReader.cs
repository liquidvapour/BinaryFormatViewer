using System.IO;

namespace BinaryFormatViewer
{
    public class ArrayFilterBytePartReader : PartReader
    {
        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            return new ArrayFilterNode(binaryReader.ReadByte());
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 13;
        }
    }
}