// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.PrimitiveTypeReaderFactoryBase
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;
using System.IO;

namespace BinaryFormatViewer
{
  [Serializable]
  public abstract class PrimitiveTypeReaderFactoryBase
  {
    public abstract bool CanRead(byte typeCode);

    public abstract Node Read(BinaryReader binaryReader);
  }
}
