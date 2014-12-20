using System;

namespace BinaryFormatViewer
{
    public static class PrimitiveTypeNodeFactories
    {
        public static PrimitiveTypeNodeFactory Boolean = new PrimitiveTypeNodeFactory(TypeCode.Boolean,
            x => new ValueNode<bool>(x.ReadBoolean()));

        public static PrimitiveTypeNodeFactory Byte = new PrimitiveTypeNodeFactory(TypeCode.Byte,
            x => new ValueNode<byte>(x.ReadByte()));

        public static PrimitiveTypeNodeFactory Int16 = new PrimitiveTypeNodeFactory(TypeCode.Int16,
            x => new ValueNode<short>(x.ReadInt16()));

        public static PrimitiveTypeNodeFactory Int32 = new PrimitiveTypeNodeFactory(TypeCode.Int32,
            x => new ValueNode<int>(x.ReadInt32()));

        public static PrimitiveTypeNodeFactory Int64 = new PrimitiveTypeNodeFactory(TypeCode.Int64,
            x => new ValueNode<long>(x.ReadInt64()));

        public static PrimitiveTypeNodeFactory DateTime = new PrimitiveTypeNodeFactory(TypeCode.DateTime,
            x => new ValueNode<DateTime>(System.DateTime.FromBinary(x.ReadInt64())));
        
        //TODO RP 2014-12-20: Add extra types (UInt16, ...)

        public static PrimitiveTypeNodeFactory UInt32 = new PrimitiveTypeNodeFactory(TypeCode.UInt32,
            x => new ValueNode<uint>(x.ReadUInt32()));

        public static PrimitiveTypeNodeFactory UInt64 = new PrimitiveTypeNodeFactory(TypeCode.UInt64,
            x => new ValueNode<ulong>(x.ReadUInt64()));
    }
}