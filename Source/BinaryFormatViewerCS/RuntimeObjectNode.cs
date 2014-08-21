﻿// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.RuntimeObjectNode
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryFormatViewer
{
  [Serializable]
  public class RuntimeObjectNode : IdentifiedNode, IHaveChildren, IHaveTypeSpecs
  {
    private Node _assembly;
    private List<FieldNode> _fieldValues;
    private string _name;

    public virtual List<Node> Values
    {
      get
      {
        List<Node> list = new List<Node>();
        using (List<FieldNode>.Enumerator enumerator = this.Fields.GetEnumerator())
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
      get
      {
        return this._fieldValues;
      }
    }

    public virtual string Name
    {
      get
      {
        return this._name;
      }
    }

    public virtual Node Assembly
    {
      get
      {
        return this._assembly;
      }
      set
      {
        this._assembly = value;
      }
    }

    public RuntimeObjectNode(uint id, string name, List<FieldNode> fields)
      : this(id, name, fields, (Node) null)
    {
    }

    public RuntimeObjectNode(uint id, string name, List<FieldNode> fields, Node assembly)
      : base(id)
    {
      this._fieldValues = fields;
      this._name = name;
      this._assembly = assembly;
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("Runtime Object");
      stringBuilder.AppendLine("--------------");
      stringBuilder.AppendLine(base.ToString());
      stringBuilder.AppendLine(new StringBuilder("Name: '").Append(this._name).Append("'").ToString());
      if (this._assembly != null)
        stringBuilder.AppendLine(new StringBuilder("Assembly: '").Append(this._assembly.ToString()).Append("'.").ToString());
      using (List<FieldNode>.Enumerator enumerator = this.Fields.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          FieldNode current = enumerator.Current;
          stringBuilder.AppendLine(new StringBuilder("field: ").Append(current.ToString()).ToString());
        }
      }
      stringBuilder.AppendLine("--------------");
      return stringBuilder.ToString();
    }
  }
}
