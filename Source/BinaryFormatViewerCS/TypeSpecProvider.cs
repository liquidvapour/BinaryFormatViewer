using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using log4net;

namespace BinaryFormatViewer
{
    [Serializable]
    public class TypeSpecProvider
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof (TypeSpecProvider));
        protected List<TypeSpecReader> _typeSpecReaders;

        public TypeSpecProvider()
        {
            _typeSpecReaders = new List<TypeSpecReader>();
            _typeSpecReaders.Add(new PrimitiveTypeSpecReader());
            _typeSpecReaders.Add(new GeneralTypeTypeSpecReader());
            _typeSpecReaders.Add(new StringTypeSpecReader());
            _typeSpecReaders.Add(new ObjectTypeSpecReader());
            _typeSpecReaders.Add(new StringArrayTypeSpecReader());
            _typeSpecReaders.Add(new RuntimeTypeSpecReader());
            _typeSpecReaders.Add(new ArrayOfPrimitiveTypeSpecReader());
            _typeSpecReaders.Add(new ArrayOfObject());
        }

        public TypeSpec GetTypeSpecFor(byte typeTag, BinaryReader binaryReader)
        {
            logger.Debug(
                new StringBuilder("Getting TypeSpec for typeTag: '").Append((object) typeTag)
                    .Append("' at position: '")
                    .Append((object) binaryReader.BaseStream.Position)
                    .Append("'.")
                    .ToString());
            using (IEnumerator<TypeSpecReader> enumerator = _typeSpecReaders.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    TypeSpecReader current = enumerator.Current;
                    if (current.CanRead(typeTag))
                    {
                        logger.Debug(
                            new StringBuilder("Found TypeSpecReader '").Append(current.GetType().FullName)
                                .Append("'.")
                                .ToString());
                        return current.Read(binaryReader);
                    }
                }
            }
            throw new ArgumentException(
                new StringBuilder("No type spec reader for type tag ").Append((object) typeTag).ToString(), "typeTag");
        }
    }
}