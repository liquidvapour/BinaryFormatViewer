using Boo.Lang;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BinaryFormatViewer
{
    [Serializable]
    public class TypeSpecProvider
    {
        private static ILog logger = LogManager.GetLogger(typeof(TypeSpecProvider));
        protected System.Collections.Generic.List<TypeSpecReader> _typeSpecReaders;

        public TypeSpecProvider()
        {
            this._typeSpecReaders = new System.Collections.Generic.List<TypeSpecReader>();
            this._typeSpecReaders.Add((TypeSpecReader)new PrimitiveTypeSpecReader());
            this._typeSpecReaders.Add((TypeSpecReader)new GeneralTypeTypeSpecReader());
            this._typeSpecReaders.Add((TypeSpecReader)new StringTypeSpecReader());
            this._typeSpecReaders.Add((TypeSpecReader)new ObjectTypeSpecReader());
            this._typeSpecReaders.Add((TypeSpecReader)new StringArrayTypeSpecReader());
            this._typeSpecReaders.Add((TypeSpecReader)new RuntimeTypeSpecReader());
            this._typeSpecReaders.Add((TypeSpecReader)new ArrayOfPrimitiveTypeSpecReader());
            this._typeSpecReaders.Add((TypeSpecReader)new ArrayOfObject());
        }

        public TypeSpec GetTypeSpecFor(byte typeTag, BinaryReader binaryReader)
        {
            TypeSpecProvider.logger.Debug((object)new StringBuilder("Getting TypeSpec for typeTag: '").Append((object)typeTag).Append("' at position: '").Append((object)binaryReader.BaseStream.Position).Append("'.").ToString());
            using (IEnumerator<TypeSpecReader> enumerator = this._typeSpecReaders.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    TypeSpecReader current = enumerator.Current;
                    if (current.CanRead(typeTag))
                    {
                        TypeSpecProvider.logger.Debug((object)new StringBuilder("Found TypeSpecReader '").Append(current.GetType().FullName).Append("'.").ToString());
                        return current.Read(binaryReader);
                    }
                }
            }
            throw new ArgumentException(new StringBuilder("No type spec reader for type tag ").Append((object)typeTag).ToString(), "typeTag");
        }
    }
}
