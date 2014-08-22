using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;

namespace BinaryFormatViewer
{
    [Serializable]
    public class PartProvider
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof (PartProvider));
        private readonly List<PartReader> _partReaders;
        private PrimitiveTypeReader _primitiveTypeReader;

        public PartProvider()
        {
            _primitiveTypeReader = new PrimitiveTypeReader();
            _partReaders = new List<PartReader>();
            _partReaders.Add(new HeaderPartReader());
            _partReaders.Add(new RefTypeObjectPartReader(this, _primitiveTypeReader));
            _partReaders.Add(new AssemblyPartReader());
            _partReaders.Add(new RuntimeObjectPartReader(this, _primitiveTypeReader));
            _partReaders.Add(new ExternalObjectPartReader(this, _primitiveTypeReader));
            _partReaders.Add(new ObjectReferencePartReader());
            _partReaders.Add(new NullValuePartReader());
            _partReaders.Add(new GenericArrayReader(this, _primitiveTypeReader));
            _partReaders.Add(new StringPartReader());
            _partReaders.Add(new ArrayOfStringPartReader(this));
            _partReaders.Add(new ArrayOfObjectPartReader(this));
            _partReaders.Add(new BoxedPrimitiveTypePartReader(_primitiveTypeReader));
            _partReaders.Add(new EndPartReader());
            _partReaders.Add(new ArrayFilterBytePartReader());
        }

        public Node ReadNextPart(BinaryReader reader, ReadContext context)
        {
            int partCode = reader.ReadByte();
            logger.Debug(
                new StringBuilder("Finding part for partCode: '").Append((object) partCode)
                    .Append("' at position: '")
                    .Append((object) reader.BaseStream.Position)
                    .Append("'.")
                    .ToString());
            PartReader partReader = GetPartReader(partCode);
            Node node = null;
            if (partReader != null)
            {
                node = partReader.Read(reader, context);
                logger.Debug(new StringBuilder("Part read:\r\n").Append(node).Append(".").ToString());
            }
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