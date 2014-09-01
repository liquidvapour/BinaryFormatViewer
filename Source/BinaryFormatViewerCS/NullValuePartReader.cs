using System.IO;

namespace BinaryFormatViewer
{
    public class NullValuePartReader : PartReader
    {
        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            return new NullNode();
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 10;
        }
    }
}