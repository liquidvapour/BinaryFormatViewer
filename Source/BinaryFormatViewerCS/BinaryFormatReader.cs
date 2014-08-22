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

        public BinaryFormatReader()
        {
            _partProvider = new PartProvider();
        }

        public BinaryFormatterOutput ReadFull(Stream stream)
        {
            logger.Info("Start ReadFull");
            var nodes = new List<Node>();
            var context = new ReadContext();
            BinaryReader reader;
            using (reader = new BinaryReader(stream))
            {
                while (true)
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

            IDictionary<uint, AssemblyNode> hash = BuildAssemblyHash(nodes);
            ResolveAssemblyReferences(nodes, hash);
            ResolveReferences(nodes);
            return new BinaryFormatterOutput(GetFirstObjectNode(nodes), hash.Values, nodes.GetIdentifiedNodes());
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


        private IDictionary<uint, IdentifiedNode> BuildObjectIdHash(IEnumerable<Node> nodes)
        {
            return nodes.GetIdentifiedNodes().ToDictionary(x =>x.Id);
        }


        private void ResolveReferences(IList<Node> nodeList)
        {
            var idNodes = BuildObjectIdHash(nodeList);
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

        private void ResolveReferences(IList<FieldNode> nodeList, IDictionary<uint, IdentifiedNode> idNodes, Stack<Node> resolutionStack,
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

        private void ResolveReferences(IList<Node> nodeList, IDictionary<uint, IdentifiedNode> idNodes, Stack<Node> resolutionStack, ref int resolves)
        {
            foreach (var index in Enumerable.Range(0, nodeList.Count))
            {
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

    public static class NodeExtentions
    {
        public static IEnumerable<IdentifiedNode> GetIdentifiedNodes(this IEnumerable<Node> nodes)
        {
            return nodes.OfType<IdentifiedNode>();
        }

        public static void ForEachIdentifiedNode(this IEnumerable<Node> nodes, Action<IdentifiedNode> expr)
        {
            nodes.ForEachNodeIn(n =>
            {
                var iNode = n as IdentifiedNode;
                if (iNode != null)
                {
                    expr(iNode);
                }
            });
        }

        public static void ForEachNodeIn(this IEnumerable<Node> nodes, Action<Node> onEachNode)
        {
            if (onEachNode == null) throw new ArgumentNullException("onEachNode");

            foreach (var n in nodes)
            {
                onEachNode(n);
                var parentNode = n as IHaveChildren;
                if (parentNode != null)
                {
                    parentNode.Values.ForEachIdentifiedNode(onEachNode);
                }
            }
        }


    }
}