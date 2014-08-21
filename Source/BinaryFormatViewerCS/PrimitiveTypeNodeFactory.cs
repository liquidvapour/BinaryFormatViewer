using log4net;
using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class PrimitiveTypeNodeFactory : PrimitiveTypeReaderFactoryBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(PrimitiveTypeNodeFactory));
        private byte _typeId;
        private Func<BinaryReader, Node> _read;

        public PrimitiveTypeNodeFactory(byte typeId, Func<BinaryReader, Node> read)
        {
            this._typeId = typeId;
            this._read = read;
        }

        public override bool CanRead(byte typeCode)
        {
            return (int)this._typeId == (int)typeCode;
        }

        public override Node Read(BinaryReader binaryReader)
        {
            Node node = this._read(binaryReader);
            PrimitiveTypeNodeFactory.logger.DebugFormat("Read Primitive value: '{0}'", (object)node);
            return node;
        }
    }
}
