using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public abstract class PrimitiveTypeReaderFactoryBase
    {
        public abstract bool CanRead(byte typeCode);

        public abstract Node Read(BinaryReader binaryReader);
    }
}
