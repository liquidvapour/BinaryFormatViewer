namespace BinaryFormatViewer
{
    public class RuntimeTypeSpec : TypeSpec
    {
        public RuntimeTypeSpec(string typeName)
        {
            TypeName = typeName;
        }

        public string TypeName { get; set; }
    }
}