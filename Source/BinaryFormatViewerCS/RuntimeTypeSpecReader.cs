using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class RuntimeTypeSpecReader : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return (int)typeTag == 3;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return (TypeSpec)new RuntimeTypeSpec(binaryReader.ReadString());
        }
    }
}
