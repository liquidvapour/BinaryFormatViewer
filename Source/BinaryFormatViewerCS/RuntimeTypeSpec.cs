using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class RuntimeTypeSpec : TypeSpec
    {
        public string TypeName { get; set; }

        public RuntimeTypeSpec(string typeName)
        {
            this.TypeName = typeName;
        }
    }
}
