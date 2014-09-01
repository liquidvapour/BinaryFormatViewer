using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;

namespace BinaryFormatViewer
{
    public class PartProvider
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof (PartProvider));
        private readonly IEnumerable<PartReader> _partReaders;

        public PartProvider()
        {
            var primitiveTypeReader = new PrimitiveTypeReader();
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
            logger.Debug("Finding part for partCode: '" + partCode + "' at position: '" + reader.BaseStream.Position + "'.");

            Node node = GetPartReader(partCode).Read(reader, context);
            logger.Debug("Part read:\r\n" + node +".");
            return node;
        }

        private PartReader GetPartReader(int partCode)
        {
            PartReader reader = _partReaders.FirstOrDefault(x => x.CanRead(partCode));

            if (reader == null)
            {
                throw new ArgumentException(string.Format("No part reader for partCode: {0}.", partCode), "partCode");
            }

            logger.DebugFormat("Found part reader '{0}' for partCode: '{1}'.", reader.GetType().FullName, partCode);
            return reader;
        }
    }
}