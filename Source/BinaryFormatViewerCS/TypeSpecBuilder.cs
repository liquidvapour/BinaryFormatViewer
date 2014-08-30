using System;
using System.IO;

namespace BinaryFormatViewer
{
    [Serializable]
    public abstract class TypeSpecBuilder
    {
        public abstract bool CanRead(byte typeTag);

        public abstract TypeSpec BuildUsing(BinaryReader binaryReader);
    }
}