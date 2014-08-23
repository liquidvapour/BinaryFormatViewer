using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryFormatViewer
{
    public class ReferenceResolver
    {
        public void ResolveReferences(IList<Node> nodeList)
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

        private IDictionary<uint, IdentifiedNode> BuildObjectIdHash(IEnumerable<Node> nodes)
        {
            return nodes.GetIdentifiedNodes().ToDictionary(x => x.Id);
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
}