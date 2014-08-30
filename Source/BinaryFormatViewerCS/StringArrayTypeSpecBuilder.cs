using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class StringArrayTypeSpecBuilder : TypeSpecBuilder
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 6;
        }

        public override TypeSpec BuildUsing(BinaryReader binaryReader)
        {
            return new StringArrayTypeSpec();
        }
    }
}