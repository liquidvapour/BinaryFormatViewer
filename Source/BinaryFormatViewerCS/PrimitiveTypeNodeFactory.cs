using System;
using System.IO;
using log4net;

namespace BinaryFormatViewer
{
    public class PrimitiveTypeNodeFactory : PrimitiveTypeReaderFactoryBase
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof (PrimitiveTypeNodeFactory));
        private readonly Func<BinaryReader, Node> _read;
        private readonly byte _typeId;

        public PrimitiveTypeNodeFactory(byte typeId, Func<BinaryReader, Node> read)
        {
            _typeId = typeId;
            _read = read;
        }

        public override bool CanRead(byte typeCode)
        {
            return _typeId == typeCode;
        }

        public override Node Read(BinaryReader binaryReader)
        {
            Node node = _read(binaryReader);
            logger.DebugFormat("Read Primitive value: '{0}'", node);
            return node;
        }
    }
}