namespace BinaryFormatViewer
{
    [System.Serializable]
    public class IdentifiedNode : Node
    {
        public IdentifiedNode(uint id)
        {
            Id = id;
        }

        public uint Id { get; private set; }

        public override string ToString()
        {
            return "Id: '" + Id + "'";
        }
    }
}