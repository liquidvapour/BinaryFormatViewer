using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfPrimitiveTypeSpec : TypeSpec
    {
        public byte TypeCode { get; set; }

        public ArrayOfPrimitiveTypeSpec(byte typeCode)
        {
            this.TypeCode = typeCode;
        }
    }
}
