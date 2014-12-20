using System.IO;

namespace BinaryFormatViewer
{
    public interface IReadPrimitiveTypeNodes
    {
        bool CanRead(byte typeCode);

        Node Read(BinaryReader binaryReader);
    }
}