using System;
using System.Collections.Generic;
using System.IO;
using Boo.Lang;
using log4net;

namespace BinaryFormatViewer
{
    [Serializable]
    public class BinaryFormatReader
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof (BinaryFormatReader));
        private readonly PartProvider _partProvider;

        public BinaryFormatReader()
        {
            _partProvider = new PartProvider();
        }

        public BinaryFormatterOutput ReadFull(Stream stream)
        {
            logger.Info("Start ReadFull");
            var nodes = new System.Collections.Generic.List<Node>();
            var context = new ReadContext();
            BinaryReader reader;
            using (reader = new BinaryReader(stream))
            {
                while (1 != 0)
                {
                    Node node = _partProvider.ReadNextPart(reader, context);
                    var identifiedNode = node as IdentifiedNode;
                    if (identifiedNode != null)
                        context.ExistingObjects.Add(identifiedNode.Id, identifiedNode);
                    nodes.Add(node);
                    if (node is EndNode)
                        break;
                }
                reader.Close();
            }
            Hash idNodes = BuildObjectIdHash(nodes);
            Hash hash = BuildAssemblyHash(nodes);
            System.Collections.Generic.List<IdentifiedNode> identifiedNodes = GetIdentifiedNodes(nodes);
            ResolveAssemblyReferences(nodes, hash);
            ResolveReferences(nodes, idNodes);
            return new BinaryFormatterOutput(GetFirstObjectNode(nodes), GetAssemblyList(hash), identifiedNodes);
        }

        public IEnumerable<AssemblyNode> GetAssemblyList(Hash assembliesHash)
        {
            // ISSUE: object of a compiler-generated type is created
            foreach (AssemblyNode i in assembliesHash.Values)
            {
                yield return i;
            }
        }

        public Node Read(Stream stream)
        {
            return ReadFull(stream).MainNode;
        }

        private Hash BuildAssemblyHash(System.Collections.Generic.List<Node> nodes)
        {
            var result = new Hash();
            // ISSUE: variable of a compiler-generated type
            ForEachNodeIn(nodes, n =>
            {
                var assNode = n as AssemblyNode;
                if (assNode != null)
                {
                    result.Add(assNode.Id, assNode);
                }
            });

            return result;
        }

        private void ResolveAssemblyReferences(System.Collections.Generic.List<Node> nodes, Hash assembliesById)
        {
        }

        private Node GetFirstObjectNode(System.Collections.Generic.List<Node> nodes)
        {
            int num = 0;
            int count = nodes.Count;
            if (count < 0)
                throw new ArgumentOutOfRangeException("max");
            while (num < count)
            {
                int index = num;
                ++num;
                if (!(nodes[index] is AssemblyNode) && !(nodes[index] is StartNode))
                    return nodes[index];
            }
            return null;
        }

        private System.Collections.Generic.List<IdentifiedNode> GetIdentifiedNodes(
            System.Collections.Generic.List<Node> nodes)
        {
            var identifiedNodes = new System.Collections.Generic.List<IdentifiedNode>();
            ForEachIdentifiedNode(nodes, n => identifiedNodes.Add(n));
            return identifiedNodes;
        }

        private Hash BuildObjectIdHash(System.Collections.Generic.List<Node> nodes)
        {
            var result = new Hash();
            ForEachIdentifiedNode(nodes, n =>
            {
                if (!result.ContainsKey(n.Id))
                {
                    result.Add(n.Id, n);
                }
            });
            return result;
        }

        private void ForEachIdentifiedNode(IEnumerable<Node> nodes, Action<IdentifiedNode> expr)
        {
            ForEachNodeIn(nodes, n =>
            {
                var iNode = n as IdentifiedNode;
                if (iNode != null)
                {
                    expr(iNode);
                }
            });
        }

        private void ForEachNodeIn(IEnumerable<Node> nodes, Action<Node> onEachNode)
        {
            if (onEachNode == null) throw new ArgumentNullException("onEachNode");

            foreach (Node n in nodes)
            {
                onEachNode(n);
                var parentNode = n as IHaveChildren;
                if (parentNode != null)
                {
                    ForEachIdentifiedNode(parentNode.Values, onEachNode);
                }
            }
        }

        private void ResolveReferences(IList<Node> nodeList, Hash idNodes)
        {
            int resolves = 0;
            bool flag = false;
            while (!flag)
            {
                ResolveReferences(nodeList, idNodes, new Stack<Node>(), ref resolves);
                if (resolves == 0)
                    flag = true;
                else
                    resolves = 0;
            }
        }

        private void ResolveReferences(IList<FieldNode> nodeList, Hash idNodes, Stack<Node> resolutionStack,
            ref int resolves)
        {
            int num = 0;
            int count = nodeList.Count;
            if (count < 0)
                throw new ArgumentOutOfRangeException("max");
            while (num < count)
            {
                int index = num;
                ++num;
                if (nodeList[index].Value is ArrayOfStringNode)
                {
                    var arrayOfStringNode = (ArrayOfStringNode) nodeList[index].Value;
                    if (!resolutionStack.Contains(arrayOfStringNode))
                    {
                        resolutionStack.Push(arrayOfStringNode);
                        ResolveReferences(arrayOfStringNode.Values, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index].Value is GenericArrayNode)
                {
                    var genericArrayNode = (GenericArrayNode) nodeList[index].Value;
                    if (!resolutionStack.Contains(genericArrayNode))
                    {
                        resolutionStack.Push(genericArrayNode);
                        ResolveReferences(genericArrayNode.Values, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index].Value is ObjectNode)
                {
                    var objectNode = nodeList[index].Value as ObjectNode;
                    if (!resolutionStack.Contains(objectNode))
                    {
                        resolutionStack.Push(objectNode);
                        ResolveReferences(objectNode.Fields, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index].Value is ObjectReferenceNode)
                {
                    var objectReferenceNode = nodeList[index].Value as ObjectReferenceNode;
                    resolves = checked(resolves + 1);
                    nodeList[index].Value = (Node) idNodes[objectReferenceNode.RefId];
                }
            }
        }

        private void ResolveReferences(IList<Node> nodeList, Hash idNodes, Stack<Node> resolutionStack, ref int resolves)
        {
            int num = 0;
            int count = nodeList.Count;
            if (count < 0)
                throw new ArgumentOutOfRangeException("max");
            while (num < count)
            {
                int index = num;
                ++num;
                if (nodeList[index] is ArrayOfStringNode)
                {
                    var arrayOfStringNode = (ArrayOfStringNode) nodeList[index];
                    if (!resolutionStack.Contains(arrayOfStringNode))
                    {
                        resolutionStack.Push(arrayOfStringNode);
                        ResolveReferences(arrayOfStringNode.Values, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index] is GenericArrayNode)
                {
                    var genericArrayNode = (GenericArrayNode) nodeList[index];
                    if (!resolutionStack.Contains(genericArrayNode))
                    {
                        resolutionStack.Push(genericArrayNode);
                        ResolveReferences(genericArrayNode.Values, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index] is ObjectNode)
                {
                    var objectNode = nodeList[index] as ObjectNode;
                    if (!resolutionStack.Contains(objectNode))
                    {
                        resolutionStack.Push(objectNode);
                        ResolveReferences(objectNode.Fields, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index] is ObjectReferenceNode)
                {
                    var objectReferenceNode = nodeList[index] as ObjectReferenceNode;
                    resolves = checked(resolves + 1);
                    nodeList[index] = (Node) idNodes[objectReferenceNode.RefId];
                }
            }
        }
    }
}