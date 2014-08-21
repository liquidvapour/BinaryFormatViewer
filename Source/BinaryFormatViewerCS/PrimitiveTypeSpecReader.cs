using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class PrimitiveTypeSpecReader : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return (int)typeTag == 0;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return (TypeSpec)new PrimitiveTypeSpec(binaryReader.ReadByte());
        }
    }
}
