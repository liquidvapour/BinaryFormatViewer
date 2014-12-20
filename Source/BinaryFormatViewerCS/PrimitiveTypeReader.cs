using System;
using System.IO;
using System.Linq;
using log4net;

namespace BinaryFormatViewer
{
    public class PrimitiveTypeReader
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (PrimitiveTypeReader));
        private readonly IReadPrimitiveTypeNodes[] _strategies;

        public PrimitiveTypeReader(IReadPrimitiveTypeNodes[] primitiveTypeReaderFactories)
        {
            _strategies = primitiveTypeReaderFactories;
        }

        public Node Read(BinaryReader binaryReader, byte typeCode)
        {
            return GetPrimitiveTypeReaderStrategy(typeCode).Read(binaryReader);
        }

        private IReadPrimitiveTypeNodes GetPrimitiveTypeReaderStrategy(byte typeCode)
        {
            var strategy = _strategies.FirstOrDefault(s => s.CanRead(typeCode));
            if (strategy == null)
            {
                throw new ArgumentException(string.Format("Unhandled type code: '{0}'.", typeCode), "typeCode");
            }
            
            Logger.DebugFormat("Found PrimitiveTypeReader '{0}' for typeCode: '{1}'.",
                strategy.GetType().FullName, typeCode);

            return strategy;
        }
    }
}