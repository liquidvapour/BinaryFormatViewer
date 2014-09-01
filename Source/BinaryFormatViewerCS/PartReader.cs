using System.IO;

namespace BinaryFormatViewer
{
    public abstract class PartReader
    {
        public abstract Node Read(BinaryReader binaryReader, ReadContext context);

        public abstract bool CanRead(int partCode);
    }
}