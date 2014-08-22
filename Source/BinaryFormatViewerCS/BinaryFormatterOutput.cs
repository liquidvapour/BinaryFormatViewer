using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [Serializable]
    public class BinaryFormatterOutput
    {
        public BinaryFormatterOutput(Node mainNode, IEnumerable<AssemblyNode> assemblies,
            IEnumerable<IdentifiedNode> identifiedNodes)
        {
            MainNode = mainNode;
            Assemblies = new List<AssemblyNode>(assemblies);
            IdentifiedNodes = new List<IdentifiedNode>(identifiedNodes);
        }

        public Node MainNode { get; private set; }

        public List<AssemblyNode> Assemblies { get; private set; }

        public IEnumerable<IdentifiedNode> IdentifiedNodes { get; private set; }
    }
}