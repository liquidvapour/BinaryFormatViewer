using System;
using System.IO;
using log4net;

namespace BinaryFormatViewer
{
    public class PrimitiveTypeNodeReader : IReadPrimitiveTypeNodes
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof (PrimitiveTypeNodeReader));
        private readonly Func<BinaryReader, Node> _read;
        private readonly byte _typeId;

        public PrimitiveTypeNodeReader(TypeCode typeId, Func<BinaryReader, Node> read)
        {
            _typeId = (byte)typeId;
            _read = read;
        }

        public bool CanRead(byte typeCode)
        {
            return _typeId == typeCode;
        }

        public Node Read(BinaryReader binaryReader)
        {
            Node node = _read(binaryReader);
            logger.DebugFormat("Read Primitive value: '{0}'", node);
            return node;
        }
    }
}