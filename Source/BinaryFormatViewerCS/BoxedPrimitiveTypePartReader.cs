using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class BoxedPrimitiveTypePartReader : PartReader
    {
        private readonly PrimitiveTypeReader _primitiveTypeReader;

        public BoxedPrimitiveTypePartReader(PrimitiveTypeReader primitiveTypeReader)
        {
            _primitiveTypeReader = primitiveTypeReader;
        }

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            return _primitiveTypeReader.Read(binaryReader);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 8;
        }
    }
}