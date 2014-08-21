using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayFilterNode : Node
    {
        protected uint _value;

        public uint NumberOfNullItems
        {
            get
            {
                return this._value;
            }
        }

        public ArrayFilterNode(uint val)
        {
            this._value = val;
        }
    }
}
