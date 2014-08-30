using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class StringTypeSpecBuilder : TypeSpecBuilder
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 1;
        }

        public override TypeSpec BuildUsing(BinaryReader binaryReader)
        {
            return new StringTypeSpec();
        }
    }
}