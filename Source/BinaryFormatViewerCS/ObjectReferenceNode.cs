// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.ObjectReferenceNode
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;
using System.Text;

namespace BinaryFormatViewer
{
  [Serializable]
  public class ObjectReferenceNode : Node
  {
    private uint _refId;

    public uint RefId
    {
      get
      {
        return this._refId;
      }
    }

    public ObjectReferenceNode(uint refId)
    {
      this._refId = refId;
    }

    public override string ToString()
    {
      return new StringBuilder("ObjectReferenceNode RefId: '").Append((object) this._refId).Append("'").ToString();
    }
  }
}
