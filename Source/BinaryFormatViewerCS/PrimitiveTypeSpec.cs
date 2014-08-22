using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class PrimitiveTypeSpec : TypeSpec
    {
        public byte TypeCode { get; set; }

        public PrimitiveTypeSpec(byte typeCode)
        {
            TypeCode = typeCode;
        }
    }
}
