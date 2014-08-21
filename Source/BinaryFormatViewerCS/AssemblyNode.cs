using System;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class AssemblyNode : IdentifiedNode
    {
        private string _name;

        public string Name
        {
            get
            {
                return this._name;
            }
        }

        public AssemblyNode(uint id, string name)
            : base(id)
        {
            this._name = name;
        }

        public override string ToString()
        {
            return new StringBuilder("AssemblyNode\r\n").Append(base.ToString()).Append("\r\nName: '").Append(this._name).Append("'").ToString();
        }
    }
}
