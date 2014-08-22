// Decompiled with JetBrains decompiler
// Type: BinaryFormatViewer.IdentifiedNode
// Assembly: BinaryFormatViewer, Version=1.0.5345.0, Culture=neutral, PublicKeyToken=null
// MVID: C8F65D2D-7140-475F-83C4-81250F3AA8B8
// Assembly location: C:\Users\ra-el\Source\Repos\BinaryFormatViewer\Source\BinaryFormatViewer\bin\Debug\BinaryFormatViewer.dll

using System;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class IdentifiedNode : Node
    {
        protected uint _id;

        public IdentifiedNode(uint id)
        {
            _id = id;
        }

        public uint Id
        {
            get { return _id; }
        }

        public override string ToString()
        {
            return new StringBuilder("Id: '").Append((object) _id).Append("'").ToString();
        }
    }
}