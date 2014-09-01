using System.IO;

namespace BinaryFormatViewer
{
    public class ObjectTypeSpecBuilder : TypeSpecBuilder
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 2;
        }

        public override TypeSpec BuildUsing(BinaryReader binaryReader)
        {
            return new ObjectTypeSpec();
        }
    }
}