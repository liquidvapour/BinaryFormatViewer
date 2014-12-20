namespace BinaryFormatViewer
{
    public class PrimitiveTypeSpec : TypeSpec, IRepresentAPrimitive
    {
        public PrimitiveTypeSpec(byte typeCode)
        {
            TypeCode = typeCode;
        }

        public byte TypeCode { get; set; }
    }
}