using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryFormatViewer
{
    public class RuntimeObjectNode : IdentifiedNode, IHaveChildren, IHaveTypeSpecs
    {
        private bool _buildingStringRepresentationForThisInstance;

        public RuntimeObjectNode(uint id, string name, IList<FieldNode> fields)
            : this(id, name, fields, null)
        {
        }

        public RuntimeObjectNode(uint id, string name, IList<FieldNode> fields, Node assembly)
            : base(id)
        {
            Fields = fields;
            Name = name;
            Assembly = assembly;
        }

        public IList<Node> Values
        {
            get
            {
                return Fields.Select(current => current.Value).ToList();
            }
        }

        public IList<FieldNode> Fields { get; private set; }

        public string Name { get; private set; }

        public Node Assembly { get; set; }

        public override string ToString()
        {
            using (var depthController = new DepthController())
            {
                var depthPadding = GetDepthPadding(depthController.Depth);

                if (_buildingStringRepresentationForThisInstance)
                {
                    return GetReferenceStringRepresentation();
                }

                _buildingStringRepresentationForThisInstance = true;

                var result = GetFullStringRepresentation(depthPadding);

                _buildingStringRepresentationForThisInstance = false;

                return result;
            }
        }

        /// <summary>
        /// RP 2014-12-20: This object wraps a static and needs commenting so 
        /// RED FLAGS. But for the moment it is only used in this one place and
        /// makes the calling code easier to read.
        /// </summary>
        private class DepthController : IDisposable
        {
            private static int _globalDepth = -1;

            public DepthController()
            {
                _globalDepth++;
            }

            public int Depth { get { return _globalDepth >= 0 ? _globalDepth : 0; } }

            public void Dispose()
            {
                _globalDepth--;
            }
        }

        private static string GetDepthPadding(int depth)
        {
            return new string(' ', 4*depth);
        }

        private string GetReferenceStringRepresentation()
        {
            return string.Format("Reference to Runtime Object: \"{0}\"", Id);
        }

        private string GetFullStringRepresentation(string linePrefix)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("\n{0}Runtime Object\n", linePrefix);
            stringBuilder.AppendFormat("{0}<-------------\n", linePrefix);
            stringBuilder.AppendFormat("{0}{1}\n", linePrefix, base.ToString());
            stringBuilder.AppendFormat("{1}Name: '{0}'\n", Name, linePrefix);
            if (Assembly != null)
                stringBuilder.AppendFormat("{1}Assembly: '{0}'.\n", Assembly, linePrefix);

            AppendFieldsTo(stringBuilder, linePrefix);

            stringBuilder.AppendFormat("{0}------------->\n", linePrefix);
            return stringBuilder.ToString();
        }

        private void AppendFieldsTo(StringBuilder stringBuilder, string linePrefix)
        {
            foreach (var field in Fields)
            {
                stringBuilder.AppendFormat("{1}field: {0}\n", field, linePrefix);
            }
        }
    }
}