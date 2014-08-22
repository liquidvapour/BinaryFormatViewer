using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryFormatViewer
{
    [Serializable]
    public abstract class ObjectReaderBase : PartReader
    {
        protected TypeSpecProvider _typeSpecProvider;
        protected PartProvider _partProvider;
        protected PrimitiveTypeReader _primitiveTypeReader;

        protected ObjectReaderBase(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
        {
            this._typeSpecProvider = new TypeSpecProvider();
            this._partProvider = partProvider;
            this._primitiveTypeReader = primitiveTypeReader;
        }

        protected Node GetNodeBy(BinaryReader binaryReader, TypeSpec typeSpec, ReadContext context)
        {
            if (typeSpec is PrimitiveTypeSpec)
            {
                PrimitiveTypeSpec primitiveTypeSpec = typeSpec as PrimitiveTypeSpec;
                return this._primitiveTypeReader.Read(binaryReader, primitiveTypeSpec.TypeCode);
            }
            else
            {
                if (!(typeSpec is ArrayOfPrimitiveTypeSpec))
                    return this._partProvider.ReadNextPart(binaryReader, context);

                ArrayOfPrimitiveTypeSpec primitiveTypeSpec = typeSpec as ArrayOfPrimitiveTypeSpec;
                return this._primitiveTypeReader.Read(binaryReader, primitiveTypeSpec.TypeCode);
            }
        }

        protected List<TypeSpec> ReadTypeSpecs(BinaryReader binaryReader, uint fieldCount)
        {
            var fieldCountInt32 = checked((int)fieldCount);
            var typeTags = new List<byte>(fieldCountInt32);

            foreach (var i in Enumerable.Range(0, fieldCountInt32))
            {
                typeTags.Add(binaryReader.ReadByte());
            }

            List<TypeSpec> typeSpecs = new List<TypeSpec>(fieldCountInt32);
            foreach (var tag in typeTags)
            {
                typeSpecs.Add(_typeSpecProvider.GetTypeSpecFor(tag, binaryReader));
            }
            return typeSpecs;
        }

        public abstract override Node Read(BinaryReader binaryReader, ReadContext context);

        public abstract override bool CanRead(int partCode);
    }
}
