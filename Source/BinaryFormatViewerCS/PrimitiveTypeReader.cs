using Boo.Lang;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class PrimitiveTypeReader
    {
        private static ILog logger = LogManager.GetLogger(typeof(PrimitiveTypeReader));
        private System.Collections.Generic.List<PrimitiveTypeReaderFactoryBase> _strategies;

        public PrimitiveTypeReader()
        {
            this._strategies = new System.Collections.Generic.List<PrimitiveTypeReaderFactoryBase>();
            this._strategies.Add((PrimitiveTypeReaderFactoryBase)PrimitiveTypeNodeFactories.Boolean);
            this._strategies.Add((PrimitiveTypeReaderFactoryBase)PrimitiveTypeNodeFactories.Byte);
            this._strategies.Add((PrimitiveTypeReaderFactoryBase)PrimitiveTypeNodeFactories.Int16);
            this._strategies.Add((PrimitiveTypeReaderFactoryBase)PrimitiveTypeNodeFactories.Int32);
            this._strategies.Add((PrimitiveTypeReaderFactoryBase)PrimitiveTypeNodeFactories.Int64);
            this._strategies.Add((PrimitiveTypeReaderFactoryBase)PrimitiveTypeNodeFactories.DateTime);
            this._strategies.Add((PrimitiveTypeReaderFactoryBase)PrimitiveTypeNodeFactories.UInt32);
            this._strategies.Add((PrimitiveTypeReaderFactoryBase)PrimitiveTypeNodeFactories.UInt64);
        }

        public Node Read(BinaryReader binaryReader, byte typeCode)
        {
            return this.GetPrimitiveTypeReaderStrategy(typeCode).Read(binaryReader);
        }

        public Node Read(BinaryReader binaryReader)
        {
            return this.GetPrimitiveTypeReaderStrategy(binaryReader.ReadByte()).Read(binaryReader);
        }

        private PrimitiveTypeReaderFactoryBase GetPrimitiveTypeReaderStrategy(byte typeCode)
        {
            foreach (var strategy in _strategies)
            {
                if (strategy.CanRead(typeCode))
                {
                    PrimitiveTypeReader.logger.DebugFormat("Found PrimitiveTypeReader '{0}' for typeCode: '{1}'.", (object)strategy.GetType().FullName, (object)typeCode);
                    return strategy;
                }
            }
            throw new ArgumentException(new StringBuilder("Unhandled type code: ").Append((object)typeCode).ToString(), "typeCode");
        }
    }
}
