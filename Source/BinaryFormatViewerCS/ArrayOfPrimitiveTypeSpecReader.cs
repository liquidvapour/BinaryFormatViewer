using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfPrimitiveTypeSpecReader : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 7;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return (TypeSpec)new ArrayOfPrimitiveTypeSpec(binaryReader.ReadByte());
        }
    }
}
