using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public class BoxedPrimitiveTypePartReader : PartReader
    {
        protected PrimitiveTypeReader _primitiveTypeReader;

        public BoxedPrimitiveTypePartReader(PrimitiveTypeReader primitiveTypeReader)
        {
            this._primitiveTypeReader = primitiveTypeReader;
        }

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            return this._primitiveTypeReader.Read(binaryReader);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 8;
        }
    }
}
