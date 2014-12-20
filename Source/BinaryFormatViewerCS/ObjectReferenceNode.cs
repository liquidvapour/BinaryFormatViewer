namespace BinaryFormatViewer
{
    public class ObjectReferenceNode : Node
    {
        private readonly uint _refId;

        public ObjectReferenceNode(uint refId)
        {
            _refId = refId;
        }

        public uint RefId
        {
            get { return _refId; }
        }

        public override string ToString()
        {
            return string.Format("ObjectReferenceNode RefId: '{0}'", _refId);
        }
    }
}