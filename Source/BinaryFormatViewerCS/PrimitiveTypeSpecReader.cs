using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class PrimitiveTypeSpecReader : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 0;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return new PrimitiveTypeSpec(binaryReader.ReadByte());
        }
    }
}