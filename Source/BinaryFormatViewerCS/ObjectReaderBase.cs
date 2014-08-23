using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryFormatViewer
{
    [Serializable]
    public abstract class ObjectReaderBase : PartReader
    {
        private readonly PartProvider _partProvider;
        private readonly PrimitiveTypeReader _primitiveTypeReader;
        private readonly TypeSpecProvider _typeSpecProvider;

        protected ObjectReaderBase(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
        {
            _typeSpecProvider = new TypeSpecProvider();
            _partProvider = partProvider;
            _primitiveTypeReader = primitiveTypeReader;
        }

        protected Node GetNodeBy(BinaryReader binaryReader, TypeSpec typeSpec, ReadContext context)
        {
            if (typeSpec is PrimitiveTypeSpec)
            {
                var primitiveTypeSpec = typeSpec as PrimitiveTypeSpec;
                return _primitiveTypeReader.Read(binaryReader, primitiveTypeSpec.TypeCode);
            }
            else
            {
                if (!(typeSpec is ArrayOfPrimitiveTypeSpec))
                    return _partProvider.ReadNextPart(binaryReader, context);

                var primitiveTypeSpec = typeSpec as ArrayOfPrimitiveTypeSpec;
                return _primitiveTypeReader.Read(binaryReader, primitiveTypeSpec.TypeCode);
            }
        }

        protected List<TypeSpec> ReadTypeSpecs(BinaryReader binaryReader, uint fieldCount)
        {
            var fieldCountInt32 = checked((int) fieldCount);
            var typeTags = Enumerable.Range(0, fieldCountInt32).Select(i => binaryReader.ReadByte()).ToList();

            return new List<TypeSpec>(typeTags.Select(tag => _typeSpecProvider.GetTypeSpecFor(tag, binaryReader)));
        }

        public abstract override Node Read(BinaryReader binaryReader, ReadContext context);

        public abstract override bool CanRead(int partCode);
    }
}