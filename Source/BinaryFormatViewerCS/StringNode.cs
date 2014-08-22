using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class StringNode : IdentifiedNode
    {
        public string Value { get; private set; }

        public StringNode(uint objectId, string value) : base(objectId)
        {
            this.Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
