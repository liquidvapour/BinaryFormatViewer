using System;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ObjectReferenceNode : Node
    {
        private uint _refId;

        public uint RefId
        {
            get
            {
                return this._refId;
            }
        }

        public ObjectReferenceNode(uint refId)
        {
            this._refId = refId;
        }

        public override string ToString()
        {
            return new StringBuilder("ObjectReferenceNode RefId: '").Append((object)this._refId).Append("'").ToString();
        }
    }
}
