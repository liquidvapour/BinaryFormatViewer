using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class StringTypeSpecReader : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 1;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return new StringTypeSpec();
        }
    }
}