using System;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class AssemblyRefNode : Node
    {
        private readonly uint _id;

        public AssemblyRefNode(uint id)
        {
            _id = id;
        }

        public uint Id
        {
            get { return _id; }
        }

        public override string ToString()
        {
            return new StringBuilder("AssemblyRef: ").Append((object) _id).ToString();
        }
    }
}