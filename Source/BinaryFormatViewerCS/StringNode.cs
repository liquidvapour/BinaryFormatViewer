using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class StringNode : IdentifiedNode
    {
        private string _val;

        public string Value
        {
            get
            {
                return this._val;
            }
        }

        public StringNode(uint objectId, string val)
            : base(objectId)
        {
            this._val = val;
        }

        public override string ToString()
        {
            return this._val;
        }
    }
}
