using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class TypeSpec
    {
        private readonly string _name;

        public TypeSpec() : this(null)
        {
        }

        public TypeSpec(string name)
        {
            _name = name ?? GetType().FullName;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}