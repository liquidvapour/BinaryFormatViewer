using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class TypeSpec
    {
        private string _name;

        public TypeSpec() : this(null) {}

        public TypeSpec(string name)
        {
            this._name = name ?? GetType().FullName;
        }

        public override string ToString()
        {
            return this._name;
        }
    }
}
