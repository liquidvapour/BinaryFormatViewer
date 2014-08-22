// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.FieldNode
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class FieldNode : Node
    {
        protected string _name;
        protected TypeSpec _typeSpec;
        protected Node _value;

        public FieldNode(string name, Node value, TypeSpec typeSpec)
        {
            _name = name;
            _value = value;
            _typeSpec = typeSpec;
        }

        public string Name
        {
            get { return _name; }
        }

        public Node Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public TypeSpec TypeSpec
        {
            get { return _typeSpec; }
        }

        public override string ToString()
        {
            return
                new StringBuilder().Append(_name)
                    .Append(" of ")
                    .Append(_typeSpec)
                    .Append(": '")
                    .Append(_value)
                    .Append("'")
                    .ToString();
        }
    }
}