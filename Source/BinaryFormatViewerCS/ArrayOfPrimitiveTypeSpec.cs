namespace BinaryFormatViewer
{
    public class ArrayOfPrimitiveTypeSpec : TypeSpec
    {
        public ArrayOfPrimitiveTypeSpec(byte typeCode)
        {
            TypeCode = typeCode;
        }

        public byte TypeCode { get; set; }
    }
}