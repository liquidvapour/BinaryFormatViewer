using Boo.Lang;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class BinaryFormatReader
    {
        private static ILog logger = LogManager.GetLogger(typeof(BinaryFormatReader));
        private PartProvider _partProvider;

        public BinaryFormatReader()
        {
            this._partProvider = new PartProvider();
        }

        public BinaryFormatterOutput ReadFull(Stream stream)
        {
            BinaryFormatReader.logger.Info((object)"Start ReadFull");
            System.Collections.Generic.List<Node> nodes = new System.Collections.Generic.List<Node>();
            ReadContext context = new ReadContext();
            BinaryReader reader;
            using ((reader = new BinaryReader(stream)) as IDisposable)
            {
                while (1 != 0)
                {
                    Node node = this._partProvider.ReadNextPart(reader, context);
                    IdentifiedNode identifiedNode = node as IdentifiedNode;
                    if (identifiedNode != null)
                        context.ExistingObjects.Add(identifiedNode.Id, (Node)identifiedNode);
                    nodes.Add(node);
                    if (node is EndNode)
                        break;
                }
                reader.Close();
            }
            Hash idNodes = this.BuildObjectIdHash(nodes);
            Hash hash = this.BuildAssemblyHash(nodes);
            System.Collections.Generic.List<IdentifiedNode> identifiedNodes = this.GetIdentifiedNodes(nodes);
            this.ResolveAssemblyReferences(nodes, hash);
            this.ResolveReferences((IList<Node>)nodes, idNodes);
            return new BinaryFormatterOutput(this.GetFirstObjectNode(nodes), this.GetAssemblyList(hash), (IEnumerable<IdentifiedNode>)identifiedNodes);
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
            return this.ReadFull(stream).MainNode;
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
            return (Node)null;
        }

        private System.Collections.Generic.List<IdentifiedNode> GetIdentifiedNodes(System.Collections.Generic.List<Node> nodes)
        {
            var identifiedNodes = new System.Collections.Generic.List<IdentifiedNode>();
            this.ForEachIdentifiedNode(nodes, n => identifiedNodes.Add(n));
            return identifiedNodes;
        }

        private Hash BuildObjectIdHash(System.Collections.Generic.List<Node> nodes)
        {
            var result = new Hash();
            this.ForEachIdentifiedNode(nodes, n =>
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
            this.ForEachNodeIn(nodes, n =>
            {
                var iNode = n as IdentifiedNode;
                if (iNode != null) { expr(iNode); }
            });
        }

        private void ForEachNodeIn(IEnumerable<Node> nodes, Action<Node> onEachNode)
        {
            if (onEachNode == null) throw new ArgumentNullException("onEachNode");

            foreach (var n in nodes)
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
                this.ResolveReferences(nodeList, idNodes, new Stack<Node>(), ref resolves);
                if (resolves == 0)
                    flag = true;
                else
                    resolves = 0;
            }
        }

        private void ResolveReferences(IList<FieldNode> nodeList, Hash idNodes, Stack<Node> resolutionStack, ref int resolves)
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
                    ArrayOfStringNode arrayOfStringNode = (ArrayOfStringNode)nodeList[index].Value;
                    if (!resolutionStack.Contains((Node)arrayOfStringNode))
                    {
                        resolutionStack.Push((Node)arrayOfStringNode);
                        this.ResolveReferences((IList<Node>)arrayOfStringNode.Values, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index].Value is GenericArrayNode)
                {
                    GenericArrayNode genericArrayNode = (GenericArrayNode)nodeList[index].Value;
                    if (!resolutionStack.Contains((Node)genericArrayNode))
                    {
                        resolutionStack.Push((Node)genericArrayNode);
                        this.ResolveReferences((IList<Node>)genericArrayNode.Values, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index].Value is ObjectNode)
                {
                    ObjectNode objectNode = nodeList[index].Value as ObjectNode;
                    if (!resolutionStack.Contains((Node)objectNode))
                    {
                        resolutionStack.Push((Node)objectNode);
                        this.ResolveReferences(objectNode.Fields as IList<FieldNode>, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index].Value is ObjectReferenceNode)
                {
                    ObjectReferenceNode objectReferenceNode = nodeList[index].Value as ObjectReferenceNode;
                    resolves = checked(resolves + 1);
                    nodeList[index].Value = (Node)idNodes[(object)objectReferenceNode.RefId];
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
                    ArrayOfStringNode arrayOfStringNode = (ArrayOfStringNode)nodeList[index];
                    if (!resolutionStack.Contains((Node)arrayOfStringNode))
                    {
                        resolutionStack.Push((Node)arrayOfStringNode);
                        this.ResolveReferences((IList<Node>)arrayOfStringNode.Values, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index] is GenericArrayNode)
                {
                    GenericArrayNode genericArrayNode = (GenericArrayNode)nodeList[index];
                    if (!resolutionStack.Contains((Node)genericArrayNode))
                    {
                        resolutionStack.Push((Node)genericArrayNode);
                        this.ResolveReferences((IList<Node>)genericArrayNode.Values, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index] is ObjectNode)
                {
                    ObjectNode objectNode = nodeList[index] as ObjectNode;
                    if (!resolutionStack.Contains((Node)objectNode))
                    {
                        resolutionStack.Push((Node)objectNode);
                        this.ResolveReferences(objectNode.Fields as IList<FieldNode>, idNodes, resolutionStack, ref resolves);
                        resolutionStack.Pop();
                    }
                }
                if (nodeList[index] is ObjectReferenceNode)
                {
                    ObjectReferenceNode objectReferenceNode = nodeList[index] as ObjectReferenceNode;
                    resolves = checked(resolves + 1);
                    nodeList[index] = (Node)idNodes[(object)objectReferenceNode.RefId];
                }
            }
        }


    }
}
