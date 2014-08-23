using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;

namespace BinaryFormatViewer
{
    [Serializable]
    public class BinaryFormatReader
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof (BinaryFormatReader));
        private readonly PartProvider _partProvider;
        private readonly ReferenceResolver _referenceResolver;

        public BinaryFormatReader()
        {
            _partProvider = new PartProvider();
            _referenceResolver = new ReferenceResolver();
        }

        public BinaryFormatterOutput ReadFull(Stream stream)
        {
            logger.Info("Start ReadFull");

            List<Node> nodes = GetNodes(stream);

            IDictionary<uint, AssemblyNode> hash = BuildAssemblyHash(nodes);
            ResolveAssemblyReferences(nodes, hash);
            _referenceResolver.ResolveReferences(nodes);
            return new BinaryFormatterOutput(GetFirstObjectNode(nodes), hash.Values, nodes.GetIdentifiedNodes());
        }

        private List<Node> GetNodes(Stream stream)
        {
            var nodes = new List<Node>();
            var context = new ReadContext();

            using (var reader = new BinaryReader(stream))
            {
                foreach (var node in _partProvider.EnumerateParts(reader, context))
                {
                    var identifiedNode = node as IdentifiedNode;
                    if (identifiedNode != null)
                        context.ExistingObjects.Add(identifiedNode.Id, identifiedNode);
                    nodes.Add(node);
                }

                reader.Close();
            }
            return nodes;
        }

        public Node Read(Stream stream)
        {
            return ReadFull(stream).MainNode;
        }

        private IDictionary<uint, AssemblyNode> BuildAssemblyHash(IEnumerable<Node> nodes)
        {
            return nodes.OfType<AssemblyNode>().ToDictionary(x => x.Id);
        }

        private static void ResolveAssemblyReferences(List<Node> nodes, IDictionary<uint, AssemblyNode> assembliesById)
        {
        }

        private static Node GetFirstObjectNode(IEnumerable<Node> nodes)
        {
            return nodes.FirstOrDefault(x => !(x is AssemblyNode) && !(x is StartNode));
        }
    }
}