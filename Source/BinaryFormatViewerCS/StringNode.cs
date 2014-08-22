using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class StringNode : IdentifiedNode
    {
        public StringNode(uint objectId, string value) : base(objectId)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public override string ToString()
        {
            return Value;
        }
    }
}