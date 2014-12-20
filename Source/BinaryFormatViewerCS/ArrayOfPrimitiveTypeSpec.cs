namespace BinaryFormatViewer
{
    public class ArrayOfPrimitiveTypeSpec : TypeSpec, IRepresentAPrimitive
    {
        public ArrayOfPrimitiveTypeSpec(byte typeCode)
        {
            TypeCode = typeCode;
        }

        public byte TypeCode { get; set; }
    }
}