using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class RuntimeTypeSpec : TypeSpec
    {
        protected string _typeName;

        public string TypeName
        {
            get
            {
                return this._typeName;
            }
            set
            {
                this._typeName = value;
            }
        }

        public RuntimeTypeSpec(string typeName)
        {
            this._typeName = typeName;
        }
    }
}
