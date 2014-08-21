using System;
using System.Collections.Generic;
using System.IO;

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
            List<int> list1 = new List<int>((int)fieldCount);
            int num1 = 0;
            int num2 = (int)fieldCount;
            int num3 = 1;
            if (num2 < num1)
                num3 = -1;
            while (num1 != num2)
            {
                num1 += num3;
                list1.Add((int)binaryReader.ReadByte());
            }
            List<TypeSpec> list2 = new List<TypeSpec>((int)fieldCount);
            using (List<int>.Enumerator enumerator = list1.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int current = enumerator.Current;
                    list2.Add(this._typeSpecProvider.GetTypeSpecFor(checked((byte)current), binaryReader));
                }
            }
            return list2;
        }

        public abstract override Node Read(BinaryReader binaryReader, ReadContext context);

        public abstract override bool CanRead(int partCode);
    }
}
