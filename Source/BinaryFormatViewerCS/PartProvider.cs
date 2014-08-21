using Boo.Lang;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace BinaryFormatViewer
{
    [Serializable]
    public class PartProvider
    {
        private static ILog logger = LogManager.GetLogger(typeof(PartProvider));
        private System.Collections.Generic.List<PartReader> _partReaders;
        private PrimitiveTypeReader _primitiveTypeReader;

        public PartProvider()
        {
            this._primitiveTypeReader = new PrimitiveTypeReader();
            this._partReaders = new System.Collections.Generic.List<PartReader>();
            this._partReaders.Add((PartReader)new HeaderPartReader());
            this._partReaders.Add((PartReader)new RefTypeObjectPartReader(this, this._primitiveTypeReader));
            this._partReaders.Add((PartReader)new AssemblyPartReader());
            this._partReaders.Add((PartReader)new RuntimeObjectPartReader(this, this._primitiveTypeReader));
            this._partReaders.Add((PartReader)new ExternalObjectPartReader(this, this._primitiveTypeReader));
            this._partReaders.Add((PartReader)new ObjectReferencePartReader());
            this._partReaders.Add((PartReader)new NullValuePartReader());
            this._partReaders.Add((PartReader)new GenericArrayReader(this, this._primitiveTypeReader));
            this._partReaders.Add((PartReader)new StringPartReader());
            this._partReaders.Add((PartReader)new ArrayOfStringPartReader(this));
            this._partReaders.Add((PartReader)new ArrayOfObjectPartReader(this));
            this._partReaders.Add((PartReader)new BoxedPrimitiveTypePartReader(this._primitiveTypeReader));
            this._partReaders.Add((PartReader)new EndPartReader());
            this._partReaders.Add((PartReader)new ArrayFilterBytePartReader());
        }

        public Node ReadNextPart(BinaryReader reader, ReadContext context)
        {
            int partCode = (int)reader.ReadByte();
            PartProvider.logger.Debug((object)new StringBuilder("Finding part for partCode: '").Append((object)partCode).Append("' at position: '").Append((object)reader.BaseStream.Position).Append("'.").ToString());
            PartReader partReader = this.GetPartReader(partCode);
            Node node = (Node)null;
            if (partReader != null)
            {
                node = partReader.Read(reader, context);
                PartProvider.logger.Debug((object)new StringBuilder("Part read:\r\n").Append((object)node).Append(".").ToString());
            }
            return node;
        }

        private PartReader GetPartReader(int partCode)
        {
            var reader = this._partReaders.FirstOrDefault(x => x.CanRead(partCode));

            if (reader == null)
            {
                throw new ArgumentException(string.Format("No part reader for partCode: {0}.", partCode), "partCode");
            }

            logger.DebugFormat("Found part reader '{0}' for partCode: '{1}'.", reader.GetType().FullName, partCode);
            return reader;
        }
    }
}
