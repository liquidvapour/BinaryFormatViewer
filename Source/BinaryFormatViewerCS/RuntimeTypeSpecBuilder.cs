using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class RuntimeTypeSpecBuilder : TypeSpecBuilder
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 3;
        }

        public override TypeSpec BuildUsing(BinaryReader binaryReader)
        {
            return new RuntimeTypeSpec(binaryReader.ReadString());
        }
    }
}