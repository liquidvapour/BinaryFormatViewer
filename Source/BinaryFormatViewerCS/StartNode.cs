using System;

namespace BinaryFormatViewer
{
    [Serializable]
    public class StartNode : Node
    {
        public override string ToString()
        {
            return "HEADER";
        }
    }
}
