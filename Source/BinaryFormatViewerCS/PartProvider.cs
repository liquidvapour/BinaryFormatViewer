using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;

namespace BinaryFormatViewer
{
    public class PartProvider
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (PartProvider));
        private readonly IEnumerable<PartReader> _partReaders;

        public PartProvider()
        {
            var primitiveTypeReader = new PrimitiveTypeReader(new IReadPrimitiveTypeNodes[]
            {
                new PrimitiveTypeNodeReader(TypeCode.Boolean, x => new ValueNode<bool>(x.ReadBoolean())),
                new PrimitiveTypeNodeReader(TypeCode.Byte, x => new ValueNode<byte>(x.ReadByte())),
                new PrimitiveTypeNodeReader(TypeCode.Int16, x => new ValueNode<short>(x.ReadInt16())),
                new PrimitiveTypeNodeReader(TypeCode.Int32, x => new ValueNode<int>(x.ReadInt32())),
                new PrimitiveTypeNodeReader(TypeCode.Int64, x => new ValueNode<long>(x.ReadInt64())),
                new PrimitiveTypeNodeReader(TypeCode.DateTime, x => new ValueNode<DateTime>(DateTime.FromBinary(x.ReadInt64()))),
                new PrimitiveTypeNodeReader(TypeCode.UInt32, x => new ValueNode<uint>(x.ReadUInt32())),
                new PrimitiveTypeNodeReader(TypeCode.UInt64, x => new ValueNode<ulong>(x.ReadUInt64()))
            });

            _partReaders = new List<PartReader>
            {
                new HeaderPartReader(),
                new RefTypeObjectPartReader(this, primitiveTypeReader),
                new AssemblyPartReader(),
                new RuntimeObjectPartReader(this, primitiveTypeReader),
                new ExternalObjectPartReader(this, primitiveTypeReader),
                new ObjectReferencePartReader(),
                new NullValuePartReader(),
                new GenericArrayReader(this, primitiveTypeReader),
                new StringPartReader(),
                new ArrayOfStringPartReader(this),
                new ArrayOfObjectPartReader(this),
                new BoxedPrimitiveTypePartReader(primitiveTypeReader),
                new EndPartReader(),
                new ArrayFilterBytePartReader()
            };
        }

        public IEnumerable<Node> EnumerateParts(BinaryReader reader, ReadContext context)
        {
            Node node = null;
            while (!(node is EndNode))
            {
                node = ReadNextPart(reader, context);
                yield return node;                
            }
        }

        public Node ReadNextPart(BinaryReader reader, ReadContext context)
        {
            int partCode = reader.ReadByte();
            Logger.Debug("Finding part for partCode: '" + partCode + "' at position: '" + reader.BaseStream.Position + "'.");

            Node node = GetPartReader(partCode).Read(reader, context);
            Logger.Debug("Part read:\r\n" + node +".");
            return node;
        }

        private PartReader GetPartReader(int partCode)
        {
            PartReader reader = _partReaders.FirstOrDefault(x => x.CanRead(partCode));

            if (reader == null)
            {
                throw new ArgumentException(string.Format("No part reader for partCode: {0}.", partCode), "partCode");
            }

            Logger.DebugFormat("Found part reader '{0}' for partCode: '{1}'.", reader.GetType().FullName, partCode);
            return reader;
        }
    }
}