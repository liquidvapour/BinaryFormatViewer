namespace BinaryFormatViewer
{
    public class AssemblyNode : IdentifiedNode
    {
        public AssemblyNode(uint id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public override string ToString()
        {
            return "AssemblyNode\r\n" + base.ToString() + "\r\nName: '" + Name + "'";
        }
    }
}