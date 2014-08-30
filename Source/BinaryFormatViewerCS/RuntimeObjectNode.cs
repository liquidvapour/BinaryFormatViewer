using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class RuntimeObjectNode : IdentifiedNode, IHaveChildren, IHaveTypeSpecs
    {
        public RuntimeObjectNode(uint id, string name, List<FieldNode> fields)
            : this(id, name, fields, null)
        {
        }

        public RuntimeObjectNode(uint id, string name, List<FieldNode> fields, Node assembly)
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

        public List<FieldNode> Fields { get; private set; }

        public string Name { get; private set; }

        public Node Assembly { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Runtime Object");
            stringBuilder.AppendLine("--------------");
            stringBuilder.AppendLine(base.ToString());
            stringBuilder.AppendLine(string.Format("Name: '{0}'", Name));
            if (Assembly != null)
                stringBuilder.AppendLine("Assembly: '" + Assembly + "'.");

            foreach (FieldNode field in Fields)
            {
                stringBuilder.AppendLine("field: " + field);
            }

            stringBuilder.AppendLine("--------------");
            return stringBuilder.ToString();
        }
    }
}