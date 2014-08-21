using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public abstract class PartReader
    {
        public abstract Node Read(BinaryReader binaryReader, ReadContext context);

        public abstract bool CanRead(int partCode);
    }
}
