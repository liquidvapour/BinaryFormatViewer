namespace BinaryFormatViewer
{
    public class TypeSpec
    {
        private readonly string _name;

        public TypeSpec(string name = null)
        {
            _name = name ?? GetType().FullName;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}