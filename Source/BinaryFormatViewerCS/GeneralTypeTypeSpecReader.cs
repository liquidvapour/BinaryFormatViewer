using System.IO;

namespace BinaryFormatViewer
{
    [System.Serializable]
    public class GeneralTypeTypeSpecReader : TypeSpecReader
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 4;
        }

        public override TypeSpec Read(BinaryReader binaryReader)
        {
            return new GeneralTypeSpec(binaryReader.ReadString(), binaryReader.ReadInt32());
        }
    }
}