using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class EndPartReader : PartReader
    {
        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            return (Node)new EndNode();
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 11;
        }
    }
}
