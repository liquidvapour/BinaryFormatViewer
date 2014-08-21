using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
    [Serializable]
    public class BinaryFormatterOutput
    {
        protected Node _mainNode;
        protected List<AssemblyNode> _assemblies;
        protected IEnumerable<IdentifiedNode> _identifiedNodes;

        public Node MainNode
        {
            get
            {
                return this._mainNode;
            }
            set
            {
                this._mainNode = value;
            }
        }

        public List<AssemblyNode> Assemblies
        {
            get
            {
                return this._assemblies;
            }
            set
            {
                this._assemblies = value;
            }
        }

        public IEnumerable<IdentifiedNode> IdentifiedNodes
        {
            get
            {
                return this._identifiedNodes;
            }
            set
            {
                this._identifiedNodes = value;
            }
        }

        public BinaryFormatterOutput(Node mainNode, IEnumerable<AssemblyNode> assemblies, IEnumerable<IdentifiedNode> identifiedNodes)
        {
            this._mainNode = mainNode;
            this._assemblies = new List<AssemblyNode>(assemblies);
            this._identifiedNodes = (IEnumerable<IdentifiedNode>)new List<IdentifiedNode>(identifiedNodes);
        }
    }
}
