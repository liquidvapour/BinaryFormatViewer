using System.IO;

namespace BinaryFormatViewer
{
    [System.Serializable]
    public class GeneralTypeTypeSpecBuilder : TypeSpecBuilder
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 4;
        }

        public override TypeSpec BuildUsing(BinaryReader binaryReader)
        {
            return new GeneralTypeSpec(binaryReader.ReadString(), binaryReader.ReadInt32());
        }
    }
}