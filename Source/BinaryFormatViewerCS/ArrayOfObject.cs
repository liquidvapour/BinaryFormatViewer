using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfObject : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 5;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return new ArrayOfObjectTypeSpec();
        }
    }
}