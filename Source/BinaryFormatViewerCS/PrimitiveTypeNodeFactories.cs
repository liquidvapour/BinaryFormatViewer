using System;
using System.IO;

namespace BinaryFormatViewer
{
  [Serializable]
  public static class PrimitiveTypeNodeFactories
  {
    public static PrimitiveTypeNodeFactory Boolean = new PrimitiveTypeNodeFactory((byte) 1, x => new ValueNode<bool>(x.ReadBoolean()));
    public static PrimitiveTypeNodeFactory Byte = new PrimitiveTypeNodeFactory((byte)2, x => new ValueNode<byte>(x.ReadByte()));
    public static PrimitiveTypeNodeFactory Int16 = new PrimitiveTypeNodeFactory((byte)7, x => new ValueNode<short>(x.ReadInt16()));
    public static PrimitiveTypeNodeFactory Int32 = new PrimitiveTypeNodeFactory((byte)8, x => new ValueNode<int>(x.ReadInt32()));
    public static PrimitiveTypeNodeFactory Int64 = new PrimitiveTypeNodeFactory((byte)9, x => new ValueNode<long>(x.ReadInt64()));
    public static PrimitiveTypeNodeFactory DateTime = new PrimitiveTypeNodeFactory((byte)13, x => new ValueNode<DateTime>(System.DateTime.FromBinary(x.ReadInt64())));
    public static PrimitiveTypeNodeFactory UInt32 = new PrimitiveTypeNodeFactory((byte)15, x => new ValueNode<uint>(x.ReadUInt32()));
    public static PrimitiveTypeNodeFactory UInt64 = new PrimitiveTypeNodeFactory((byte)16, x => new ValueNode<ulong>(x.ReadUInt64()));

  }
}
