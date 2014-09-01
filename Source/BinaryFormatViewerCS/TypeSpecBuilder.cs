using System.IO;

namespace BinaryFormatViewer
{
    public abstract class TypeSpecBuilder
    {
        public abstract bool CanRead(byte typeTag);

        public abstract TypeSpec BuildUsing(BinaryReader binaryReader);
    }
}