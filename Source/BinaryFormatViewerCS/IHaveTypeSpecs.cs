// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.IHaveTypeSpecs
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System.Collections.Generic;

namespace BinaryFormatViewer
{
  public interface IHaveTypeSpecs
  {
    string Name { get; }

    Node Assembly { get; }

    List<FieldNode> Fields { get; }
  }
}
