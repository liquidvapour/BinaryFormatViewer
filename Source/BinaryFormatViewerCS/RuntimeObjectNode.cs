using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryFormatViewer
{
    public class RuntimeObjectNode : IdentifiedNode, IHaveChildren, IHaveTypeSpecs
    {
        private bool _buildingStringInformation;

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


        private static int _toStringDepth = -1;

        public override string ToString()
        {
            _toStringDepth++;

            var depthPadding = new string(' ', 4*(_toStringDepth >= 0 ? _toStringDepth : 0));

            if (_buildingStringInformation)
            {
                _toStringDepth--;
                return string.Format("Reference to Runtime Object: \"{0}\"", Id);
            }

            var stringBuilder = new StringBuilder();
            using (WhileBuildingStringRepresentation())
            {
                stringBuilder.AppendFormat("\n{0}Runtime Object\n", depthPadding);
                stringBuilder.AppendFormat("{0}<-------------\n", depthPadding);
                stringBuilder.AppendFormat("{0}{1}\n", depthPadding, base.ToString());
                stringBuilder.AppendFormat("{1}Name: '{0}'\n", Name, depthPadding);
                if (Assembly != null)
                    stringBuilder.AppendFormat("{1}Assembly: '{0}'.\n", Assembly, depthPadding);

                foreach (var field in Fields)
                {
                    stringBuilder.AppendFormat("{1}field: {0}\n", field, depthPadding);
                }

                stringBuilder.AppendFormat("{0}------------->\n", depthPadding);
            }
            _toStringDepth--;
            return stringBuilder.ToString();
        }

        private IDisposable WhileBuildingStringRepresentation()
        {
            return new BuildingStringControler(this);
        }

        private class BuildingStringControler : IDisposable
        {
            private readonly RuntimeObjectNode _owner;

            public BuildingStringControler(RuntimeObjectNode owner)
            {
                if (owner == null)
                {
                    throw new ArgumentNullException("owner");
                }
                _owner = owner;
                _owner._buildingStringInformation = true;
            }

            public void Dispose()
            {
                _owner._buildingStringInformation = false;
            }
        }
    }
}