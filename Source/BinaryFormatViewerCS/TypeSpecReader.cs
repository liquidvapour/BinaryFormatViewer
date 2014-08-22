using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public abstract class TypeSpecReader
    {
        public abstract bool CanRead(byte typeTag);

        public abstract TypeSpec Read(BinaryReader binaryReader);
    }
}