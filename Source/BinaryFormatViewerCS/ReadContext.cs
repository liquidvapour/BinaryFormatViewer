// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.ReadContext
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;
using System.Collections.Generic;

namespace BinaryFormatViewer
{
  [Serializable]
  public class ReadContext
  {
    private IDictionary<uint, Node> _existingObjects;

    public IDictionary<uint, Node> ExistingObjects
    {
      get
      {
        return this._existingObjects;
      }
      set
      {
        this._existingObjects = value;
      }
    }

    public ReadContext()
    {
      this._existingObjects = (IDictionary<uint, Node>) new Dictionary<uint, Node>();
    }
  }
}
