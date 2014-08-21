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
    protected Node _value;
    protected string _name;
    protected TypeSpec _typeSpec;

    public string Name
    {
      get
      {
        return this._name;
      }
    }

    public Node Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = value;
      }
    }

    public TypeSpec TypeSpec
    {
      get
      {
        return this._typeSpec;
      }
    }

    public FieldNode(string name, Node value, TypeSpec typeSpec)
    {
      this._name = name;
      this._value = value;
      this._typeSpec = typeSpec;
    }

    public override string ToString()
    {
      return new StringBuilder().Append(this._name).Append(" of ").Append((object) this._typeSpec).Append(": '").Append((object) this._value).Append("'").ToString();
    }
  }
}
