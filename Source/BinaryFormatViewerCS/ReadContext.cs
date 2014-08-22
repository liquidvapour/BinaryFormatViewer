using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ReadContext
    {
        public IDictionary<uint, Node> ExistingObjects { get; set; }

        public ReadContext()
        {
            ExistingObjects = (IDictionary<uint, Node>)new Dictionary<uint, Node>();
        }
    }
}
