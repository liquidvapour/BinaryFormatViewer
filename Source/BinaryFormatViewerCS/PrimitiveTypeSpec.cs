// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.PrimitiveTypeSpec
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;

namespace BinaryFormatViewer
{
  [Serializable]
  public class PrimitiveTypeSpec : TypeSpec
  {
    protected byte _typeCode;

    public byte TypeCode
    {
      get
      {
        return this._typeCode;
      }
      set
      {
        this._typeCode = value;
      }
    }

    public PrimitiveTypeSpec(byte typeCode)
    {
      this._typeCode = typeCode;
    }
  }
}
