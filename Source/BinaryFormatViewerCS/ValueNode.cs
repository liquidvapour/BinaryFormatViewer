using System;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ValueNode<T> : Node
    {
        protected T _value;

        public T Value
        {
            get
            {
                return this._value;
            }
        }

        public ValueNode(T va)
        {
            this._value = va;
        }

        public override string ToString()
        {
            return new StringBuilder("ValueNode of type '").Append(this.GetType().Name).Append("' with value: '").Append(this._value.ToString()).ToString();
        }
    }
}
