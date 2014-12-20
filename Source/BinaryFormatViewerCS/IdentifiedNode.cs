namespace BinaryFormatViewer
{
    public class IdentifiedNode : Node
    {
        public IdentifiedNode(uint id)
        {
            Id = id;
        }

        public uint Id { get; private set; }

        public override string ToString()
        {
            return string.Format("Id: '{0}'", Id);
        }
    }
}