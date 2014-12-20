using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net;

namespace BinaryFormatViewer
{
    public class PrimitiveTypeReader
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof (PrimitiveTypeReader));
        private readonly List<PrimitiveTypeReaderFactoryBase> _strategies;

        public PrimitiveTypeReader()
        {
            _strategies = new List<PrimitiveTypeReaderFactoryBase>
            {
                PrimitiveTypeNodeFactories.Boolean,
                PrimitiveTypeNodeFactories.Byte,
                PrimitiveTypeNodeFactories.Int16,
                PrimitiveTypeNodeFactories.Int32,
                PrimitiveTypeNodeFactories.Int64,
                PrimitiveTypeNodeFactories.DateTime,
                PrimitiveTypeNodeFactories.UInt32,
                PrimitiveTypeNodeFactories.UInt64
            };
        }

        public Node Read(BinaryReader binaryReader, byte typeCode)
        {
            return GetPrimitiveTypeReaderStrategy(typeCode).Read(binaryReader);
        }

        public Node Read(BinaryReader binaryReader)
        {
            return GetPrimitiveTypeReaderStrategy(binaryReader.ReadByte()).Read(binaryReader);
        }

        private PrimitiveTypeReaderFactoryBase GetPrimitiveTypeReaderStrategy(byte typeCode)
        {
            foreach (PrimitiveTypeReaderFactoryBase strategy in _strategies)
            {
                if (strategy.CanRead(typeCode))
                {
                    logger.DebugFormat("Found PrimitiveTypeReader '{0}' for typeCode: '{1}'.",
                        strategy.GetType().FullName, typeCode);
                    return strategy;
                }
            }
            throw new ArgumentException(
                new StringBuilder("Unhandled type code: ").Append((object) typeCode).ToString(), "typeCode");
        }
    }
}