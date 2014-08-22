﻿using System;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ObjectReferenceNode : Node
    {
        private readonly uint _refId;

        public ObjectReferenceNode(uint refId)
        {
            _refId = refId;
        }

        public uint RefId
        {
            get { return _refId; }
        }

        public override string ToString()
        {
            return new StringBuilder("ObjectReferenceNode RefId: '").Append((object) _refId).Append("'").ToString();
        }
    }
}