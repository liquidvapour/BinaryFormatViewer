using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class RuntimeTypeSpecReader : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 3;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return new RuntimeTypeSpec(binaryReader.ReadString());
        }
    }
}
