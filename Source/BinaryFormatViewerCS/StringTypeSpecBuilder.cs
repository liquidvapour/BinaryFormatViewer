using System.IO;

namespace BinaryFormatViewer
{
    public class StringTypeSpecBuilder : TypeSpecBuilder
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 1;
        }

        public override TypeSpec BuildUsing(BinaryReader binaryReader)
        {
            return new StringTypeSpec();
        }
    }
}