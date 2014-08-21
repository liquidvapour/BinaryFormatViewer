// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.NullValuePartReader
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;
using System.IO;

namespace BinaryFormatViewer
{
  [Serializable]
  public class NullValuePartReader : PartReader
  {
    public override Node Read(BinaryReader binaryReader, ReadContext context)
    {
      return (Node) new NullNode();
    }

    public override bool CanRead(int partCode)
    {
      return partCode == 10;
    }
  }
}
