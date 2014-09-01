using System.Collections.Generic;

namespace BinaryFormatViewer
{
    public class ReadContext
    {
        public ReadContext()
        {
            ExistingObjects = new Dictionary<uint, Node>();
        }

        public IDictionary<uint, Node> ExistingObjects { get; set; }
    }
}