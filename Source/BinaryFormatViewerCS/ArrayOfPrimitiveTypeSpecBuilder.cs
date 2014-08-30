using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfPrimitiveTypeSpecBuilder : TypeSpecBuilder
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 7;
        }

        public override TypeSpec BuildUsing(BinaryReader binaryReader)
        {
            return new ArrayOfPrimitiveTypeSpec(binaryReader.ReadByte());
        }
    }
}