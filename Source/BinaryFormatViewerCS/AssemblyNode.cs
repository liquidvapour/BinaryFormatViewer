using System;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class AssemblyNode : IdentifiedNode
    {
        private readonly string _name;

        public AssemblyNode(uint id, string name)
            : base(id)
        {
            _name = name;
        }

        public string Name
        {
            get { return _name; }
        }

        public override string ToString()
        {
            return
                new StringBuilder("AssemblyNode\r\n").Append(base.ToString())
                    .Append("\r\nName: '")
                    .Append(_name)
                    .Append("'")
                    .ToString();
        }
    }
}