using System;
using System.Collections.Generic;
using System.Linq;

namespace BinaryFormatViewer
{
    public static class ReferenceResolver
    {
        private static readonly IDictionary<Type, Action<Node, ResolutionContext>> ResolversBy = new Dictionary<Type, Action<Node, ResolutionContext>>
        {
            {typeof(ArrayOfStringNode), ResolveReferencesInAParentNode},
            {typeof(GenericArrayNode), ResolveReferencesInAParentNode},
            {typeof(ObjectNode), ResolveReferencesInANodeWithTypeSpecs}
        };

        public static void ResolveReferences(this IList<Node> nodeList)
        {
            nodeList.ResolveReferencesTillThereAreNoneLeftToResolve(nodeList.BuildObjectIdHashFrom());
        }

        private static void ResolveReferencesTillThereAreNoneLeftToResolve(this IList<Node> nodeList, IDictionary<uint, IdentifiedNode> idNodes)
        {
            var context = new ResolutionContext(idNodes);
            do
            {
                context.SkipedResolves = 0;
                nodeList.ResolveNodeReferences(context);
            } while (context.SkipedResolves != 0);
        }

        /// <summary>
        /// Fields that have a value that references a ReferenceNode will have value set to the actual Node.
        /// </summary>
        /// <param name="fieldNodeList"></param>
        /// <param name="context"></param>
        private static void ResolveFieldNodeReferences(this IList<FieldNode> fieldNodeList, ResolutionContext context)
        {
            fieldNodeList.Select(x => x.Value).DeepResolveAllReferences(context);

            fieldNodeList.ResolveFieldNodeObjectReferences(context);
        }


        /// <summary>
        /// references to ReferenceNode in this list will be replaced with the actual Node.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="context"></param>
        private static void ResolveNodeReferences(this IList<Node> nodeList, ResolutionContext context)
        {
            nodeList.DeepResolveAllReferences(context);

            nodeList.ResolveNodeListObjectReferences(context);
        }

        private static void ResolveFieldNodeObjectReferences(this IEnumerable<FieldNode> fieldNodeList, ResolutionContext context)
        {
            foreach (var fieldNode in fieldNodeList)
            {
                var node = fieldNode.Value as ObjectReferenceNode;
                if (node != null)
                {
                    fieldNode.Value = context.IdNodes[node.RefId];
                }
            }
        }

        private static void ResolveNodeListObjectReferences(this IList<Node> nodeList, ResolutionContext context)
        {
            foreach (var i in Enumerable.Range(0, nodeList.Count))
            {
                var node = nodeList[i] as ObjectReferenceNode;
                if (node != null)
                {
                    nodeList[i] = context.IdNodes[node.RefId];
                }
            }
        }

        private static void DeepResolveAllReferences(this IEnumerable<Node> nodeList, ResolutionContext context)
        {
            foreach (var node in nodeList)
            {
                node.ResolveDeepReferencesWith(context);
            }
        }

        private static void ResolveDeepReferencesWith(this Node node, ResolutionContext context)
        {
            var nodeType = node.GetType();
            if (!ResolversBy.ContainsKey(nodeType)) return;

            ResolversBy[nodeType](node, context);
        }

        private static void ResolveReferencesInANodeWithTypeSpecs(Node node, ResolutionContext context)
        {
            node.TrackNodeAnd(context, () => ResolveFieldNodeReferences(((IHaveTypeSpecs) node).Fields, context));
        }


        private static void ResolveReferencesInAParentNode(Node node, ResolutionContext context)
        {
            node.TrackNodeAnd(context, () => ResolveNodeReferences(((IHaveChildren)node).Children, context));
        }

        private static void TrackNodeAnd(this Node node, ResolutionContext context, Action doIt)
        {
            if (context.ResolutionStack.Contains(node))
            {
                context.SkipedResolves++;
                return;
            }

            context.ResolutionStack.Push(node);
            doIt();
            context.ResolutionStack.Pop();
        }

        private static IDictionary<uint, IdentifiedNode> BuildObjectIdHashFrom(this IEnumerable<Node> nodes)
        {
            return nodes.GetIdentifiedNodes().ToDictionary(x => x.Id);
        }
    }
}