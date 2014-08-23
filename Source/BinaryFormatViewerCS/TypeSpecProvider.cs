using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;

namespace BinaryFormatViewer
{
    [Serializable]
    public class TypeSpecProvider
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (TypeSpecProvider));

        private readonly List<TypeSpecReader> _typeSpecReaders;

        public TypeSpecProvider()
        {
            _typeSpecReaders = new List<TypeSpecReader>
            {
                new PrimitiveTypeSpecReader(),
                new GeneralTypeTypeSpecReader(),
                new StringTypeSpecReader(),
                new ObjectTypeSpecReader(),
                new StringArrayTypeSpecReader(),
                new RuntimeTypeSpecReader(),
                new ArrayOfPrimitiveTypeSpecReader(),
                new ArrayOfObject()
            };
        }

        public TypeSpec GetTypeSpecFor(byte typeTag, BinaryReader binaryReader)
        {
            Logger.Debug("Getting TypeSpec for typeTag: '" + typeTag + "' at position: '" + binaryReader.BaseStream.Position + "'.");

            return GetTypeSpecReaderFor(typeTag).Read(binaryReader);
        }

        private TypeSpecReader GetTypeSpecReaderFor(byte typeTag)
        {
            var reader = _typeSpecReaders.FirstOrDefault(x => x.CanRead(typeTag));

            if (reader == null) throw new ArgumentException("No type spec reader for type tag " + typeTag + ".", "typeTag");

            Logger.Debug("Found TypeSpecReader '" + reader.GetType().FullName + "'.");
            return reader;
        }
    }
}