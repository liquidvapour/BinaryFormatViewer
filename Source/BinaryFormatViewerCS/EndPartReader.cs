using System.IO;

namespace BinaryFormatViewer
{
    public class EndPartReader : PartReader
    {
        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            return new EndNode();
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 11;
        }
    }
}