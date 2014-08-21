// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.RuntimeObjectPartReader
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace BinaryFormatViewer
{
  [Serializable]
  public class RuntimeObjectPartReader : ObjectReaderBase
  {
    public RuntimeObjectPartReader(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
      : base(partProvider, primitiveTypeReader)
    {
    }

    public override Node Read(BinaryReader binaryReader, ReadContext context)
    {
      uint id = binaryReader.ReadUInt32();
      string name = binaryReader.ReadString();
      uint fieldCount = binaryReader.ReadUInt32();
      List<string> list1 = new List<string>((int) fieldCount);
      int num1 = 0;
      int num2 = (int) fieldCount;
      int num3 = 1;
      if (num2 < num1)
        num3 = -1;
      while (num1 != num2)
      {
        num1 += num3;
        list1.Add(binaryReader.ReadString());
      }
      List<TypeSpec> list2 = this.ReadTypeSpecs(binaryReader, fieldCount);
      List<Node> list3 = new List<Node>();
      using (List<TypeSpec>.Enumerator enumerator = list2.GetEnumerator())
      {
        while (enumerator.MoveNext())
        {
          TypeSpec current = enumerator.Current;
          list3.Add(this.GetNodeBy(binaryReader, current, context));
        }
      }
      List<FieldNode> fields = new List<FieldNode>();
      int num4 = 0;
      int count = list1.Count;
      if (count < 0)
        throw new ArgumentOutOfRangeException("max");
      while (num4 < count)
      {
        int index = num4;
        ++num4;
        fields.Add(new FieldNode(list1[index], list3[index], list2[index]));
      }
      return (Node) new RuntimeObjectNode(id, name, fields);
    }

    public override bool CanRead(int partCode)
    {
      return partCode == 4;
    }
  }
}
