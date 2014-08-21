// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.RefTypeObjectPartReader
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;
using System.Collections.Generic;
using System.IO;

namespace BinaryFormatViewer
{
  [Serializable]
  public class RefTypeObjectPartReader : ObjectReaderBase
  {
    public RefTypeObjectPartReader(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
      : base(partProvider, primitiveTypeReader)
    {
    }

    public override Node Read(BinaryReader binaryReader, ReadContext context)
    {
      uint id = binaryReader.ReadUInt32();
      uint index1 = binaryReader.ReadUInt32();
      IHaveTypeSpecs haveTypeSpecs = (IHaveTypeSpecs) context.ExistingObjects[index1];
      int num1 = 0;
      AssemblyRefNode assemblyRefNode = haveTypeSpecs.Assembly as AssemblyRefNode;
      if (assemblyRefNode != null)
      {
        num1 = (int) assemblyRefNode.Id;
      }
      else
      {
        AssemblyNode assemblyNode = haveTypeSpecs.Assembly as AssemblyNode;
        if (assemblyNode != null)
          num1 = (int) assemblyNode.Id;
      }
      Dictionary<string, Node> dictionary = new Dictionary<string, Node>();
      int num2 = 0;
      int count = haveTypeSpecs.Fields.Count;
      if (count < 0)
        throw new ArgumentOutOfRangeException("max");
      while (num2 < count)
      {
        int index2 = num2;
        ++num2;
        dictionary.Add(haveTypeSpecs.Fields[index2].Name, this.GetNodeBy(binaryReader, haveTypeSpecs.Fields[index2].TypeSpec, context));
      }
      return (Node) new ObjectNode(id, haveTypeSpecs.Name, checked ((uint) num1), haveTypeSpecs.Fields);
    }

    public override bool CanRead(int partCode)
    {
      return partCode == 1;
    }
  }
}
