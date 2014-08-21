using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfObject : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return (int)typeTag == 5;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return (TypeSpec)new ArrayOfObjectTypeSpec();
        }
    }
}
