using System.IO;

namespace BinaryFormatViewer
{
    public class ArrayOfObjectTypeSpecBuilder : TypeSpecBuilder
    {
        public override bool CanRead(byte typeTag)
        {
            return typeTag == 5;
        }

        public override TypeSpec BuildUsing(BinaryReader binaryReader)
        {
            return new ArrayOfObjectTypeSpec();
        }
    }
}