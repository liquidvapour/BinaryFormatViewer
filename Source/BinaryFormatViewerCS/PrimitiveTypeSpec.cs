namespace BinaryFormatViewer
{
    public class PrimitiveTypeSpec : TypeSpec
    {
        public PrimitiveTypeSpec(byte typeCode)
        {
            TypeCode = typeCode;
        }

        public byte TypeCode { get; set; }
    }
}