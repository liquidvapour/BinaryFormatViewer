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
        protected int _assemblyId;
        protected string _typeName;

        public GeneralTypeSpec(string typeName, int assemblyId)
        {
            _typeName = typeName;
            _assemblyId = assemblyId;
        }

        public string TypeName
        {
            get { return _typeName; }
            set { _typeName = value; }
        }

        public int AssemblyId
        {
            get { return _assemblyId; }
            set { _assemblyId = value; }
        }
    }
}