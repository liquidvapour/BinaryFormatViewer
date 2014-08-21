using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ReadContext
    {
        private IDictionary<uint, Node> _existingObjects;

        public IDictionary<uint, Node> ExistingObjects
        {
            get
            {
                return this._existingObjects;
            }
            set
            {
                this._existingObjects = value;
            }
        }

        public ReadContext()
        {
            this._existingObjects = (IDictionary<uint, Node>)new Dictionary<uint, Node>();
        }
    }
}
