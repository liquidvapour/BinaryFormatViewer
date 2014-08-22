using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class RuntimeTypeSpec : TypeSpec
    {
        public RuntimeTypeSpec(string typeName)
        {
            TypeName = typeName;
        }

        public string TypeName { get; set; }
    }
}