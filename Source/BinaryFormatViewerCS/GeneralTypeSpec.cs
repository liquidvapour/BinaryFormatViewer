// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.GeneralTypeSpec
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;

namespace BinaryFormatViewer
{
  [Serializable]
  public class GeneralTypeSpec : TypeSpec
  {
    protected string _typeName;
    protected int _assemblyId;

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

    public int AssemblyId
    {
      get
      {
        return this._assemblyId;
      }
      set
      {
        this._assemblyId = value;
      }
    }

    public GeneralTypeSpec(string typeName, int assemblyId)
    {
      this._typeName = typeName;
      this._assemblyId = assemblyId;
    }
  }
}
