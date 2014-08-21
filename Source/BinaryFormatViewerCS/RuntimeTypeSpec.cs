// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.RuntimeTypeSpec
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;

namespace BinaryFormatViewer
{
  [Serializable]
  public class RuntimeTypeSpec : TypeSpec
  {
    protected string _typeName;

    public string TypeName
    {
      get
      {
        return this._typeName;
      }
      set
      {
        this._typeName = value;
      }
    }

    public RuntimeTypeSpec(string typeName)
    {
      this._typeName = typeName;
    }
  }
}
