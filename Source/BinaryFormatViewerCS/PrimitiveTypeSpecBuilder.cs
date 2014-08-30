using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class PrimitiveTypeSpecBuilder : TypeSpecBuilder
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 0;
        }

        public override TypeSpec BuildUsing(BinaryReader binaryReader)
        {
            return new PrimitiveTypeSpec(binaryReader.ReadByte());
        }
    }
}