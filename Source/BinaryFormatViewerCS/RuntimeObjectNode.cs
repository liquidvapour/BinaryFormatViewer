using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class RuntimeObjectNode : IdentifiedNode, IHaveChildren, IHaveTypeSpecs
    {
        private readonly List<FieldNode> _fieldValues;
        private readonly string _name;
        private Node _assembly;

        public RuntimeObjectNode(uint id, string name, List<FieldNode> fields)
            : this(id, name, fields, null)
        {
        }

        public RuntimeObjectNode(uint id, string name, List<FieldNode> fields, Node assembly)
            : base(id)
        {
            _fieldValues = fields;
            _name = name;
            _assembly = assembly;
        }

        public virtual List<Node> Values
        {
            get
            {
                var list = new List<Node>();
                using (List<FieldNode>.Enumerator enumerator = Fields.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        FieldNode current = enumerator.Current;
                        list.Add(current.Value);
                    }
                }
                return list;
            }
        }

        public virtual List<FieldNode> Fields
        {
            get { return _fieldValues; }
        }

        public virtual string Name
        {
            get { return _name; }
        }

        public virtual Node Assembly
        {
            get { return _assembly; }
            set { _assembly = value; }
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Runtime Object");
            stringBuilder.AppendLine("--------------");
            stringBuilder.AppendLine(base.ToString());
            stringBuilder.AppendLine(string.Format("Name: '{0}'", _name));
            if (_assembly != null)
                stringBuilder.AppendLine("Assembly: '" + _assembly + "'.");

            foreach (FieldNode current in Fields)
            {
                stringBuilder.AppendLine(new StringBuilder("field: ").Append(current).ToString());
            }

            stringBuilder.AppendLine("--------------");
            return stringBuilder.ToString();
        }
    }
}