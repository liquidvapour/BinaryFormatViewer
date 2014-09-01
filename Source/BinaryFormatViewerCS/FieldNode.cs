namespace BinaryFormatViewer
{
    public class FieldNode : Node
    {
        public FieldNode(string name, Node value, TypeSpec typeSpec)
        {
            Name = name;
            Value = value;
            TypeSpec = typeSpec;
        }

        public string Name { get; private set; }

        public Node Value { get; set; }

        public TypeSpec TypeSpec { get; private set; }

        public override string ToString()
        {
            return Name + " of " + TypeSpec + ": '" + Value + "'";
        }
    }
}