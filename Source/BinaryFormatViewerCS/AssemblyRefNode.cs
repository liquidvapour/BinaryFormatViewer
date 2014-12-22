namespace BinaryFormatViewer
{
    public class AssemblyRefNode : Node
    {
        public AssemblyRefNode(uint assemblyId)
        {
            AssemblyId = assemblyId;
        }

        public uint AssemblyId { get; private set; }

        public override string ToString()
        {
            return string.Format("AssemblyRef: {0}", AssemblyId);
        }
    }
}