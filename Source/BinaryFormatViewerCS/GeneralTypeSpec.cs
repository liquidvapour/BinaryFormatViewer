namespace BinaryFormatViewer
{
    public class GeneralTypeSpec : TypeSpec
    {
        public GeneralTypeSpec(string typeName, int assemblyId)
        {
            TypeName = typeName;
            AssemblyId = assemblyId;
        }

        public string TypeName { get; set; }

        public int AssemblyId { get; set; }
    }
}