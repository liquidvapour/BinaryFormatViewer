namespace BinaryFormatViewer
{
    public class ValueNode<T> : Node
    {
        private readonly T _value;

        public ValueNode(T value)
        {
            _value = value;
        }

        public T Value
        {
            get { return _value; }
        }

        public override string ToString()
        {
            return string.Format("ValueNode of type '{0}' with value: '{1}", GetType().Name, _value);
        }
    }
}