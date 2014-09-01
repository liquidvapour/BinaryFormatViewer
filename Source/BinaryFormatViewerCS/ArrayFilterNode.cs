namespace BinaryFormatViewer
{
    public class ArrayFilterNode : Node
    {
        private readonly uint _value;

        public ArrayFilterNode(uint val)
        {
            _value = val;
        }

        public uint NumberOfNullItems
        {
            get { return _value; }
        }
    }
}