using System.IO;

namespace BinaryFormatViewer
{
    public class HeaderPartReader : PartReader
    {
        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            binaryReader.ReadBytes(16);
            return new StartNode();
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 0;
        }
    }
}