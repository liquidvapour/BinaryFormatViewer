using System.Collections.Generic;

namespace BinaryFormatViewer
{
    internal class ResolutionContext
    {
        public ResolutionContext(IDictionary<uint, IdentifiedNode> idNodes)
            : this(idNodes, new Stack<Node>()) {}

        private ResolutionContext(IDictionary<uint, IdentifiedNode> idNodes, Stack<Node> resolutionStack)
        {
            ResolutionStack = resolutionStack;
            IdNodes = idNodes;
        }

        public IDictionary<uint, IdentifiedNode> IdNodes { get; private set; }
        public Stack<Node> ResolutionStack { get; private set; }
        public int Resolves { get; set; }
    }
}