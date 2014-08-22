using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
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