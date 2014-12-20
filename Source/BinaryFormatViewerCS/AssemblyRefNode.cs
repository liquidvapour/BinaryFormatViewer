namespace BinaryFormatViewer
{
    public class AssemblyRefNode : Node
    {
        public AssemblyRefNode(uint id)
        {
            Id = id;
        }

        public uint Id { get; private set; }

        public override string ToString()
        {
            return string.Format("AssemblyRef: {0}", Id);
        }
    }
}