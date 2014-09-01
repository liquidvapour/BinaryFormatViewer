using System.Text;

namespace BinaryFormatViewer
{
    public class ValueNode<T> : Node
    {
        protected T _value;

        public ValueNode(T va)
        {
            _value = va;
        }

        public T Value
        {
            get { return _value; }
        }

        public override string ToString()
        {
            return
                new StringBuilder("ValueNode of type '").Append(GetType().Name)
                    .Append("' with value: '")
                    .Append(_value)
                    .ToString();
        }
    }
}