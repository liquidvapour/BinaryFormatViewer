using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ObjectTypeSpecReader : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return (int)typeTag == 2;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return (TypeSpec)new ObjectTypeSpec();
        }
    }
}
