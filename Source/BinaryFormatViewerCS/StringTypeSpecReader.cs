using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class StringTypeSpecReader : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return (int)typeTag == 1;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return (TypeSpec)new StringTypeSpec();
        }
    }
}
