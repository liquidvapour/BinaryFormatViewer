using System;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class AssemblyRefNode : Node
    {
        private uint _id;

        public uint Id
        {
            get
            {
                return this._id;
            }
        }

        public AssemblyRefNode(uint id)
        {
            this._id = id;
        }

        public override string ToString()
        {
            return new StringBuilder("AssemblyRef: ").Append((object)this._id).ToString();
        }
    }
}
