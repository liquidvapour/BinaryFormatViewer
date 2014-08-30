using System;
using System.IO;
using System.Linq;
using log4net;

namespace BinaryFormatViewer
{
    [Serializable]
    public class TypeSpecProvider
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof (TypeSpecProvider));

        private readonly TypeSpecBuilder[] _typeSpecBuilders;

        public TypeSpecProvider()
        {
            _typeSpecBuilders = new TypeSpecBuilder[]
            {
                new PrimitiveTypeSpecBuilder(),
                new GeneralTypeTypeSpecBuilder(),
                new StringTypeSpecBuilder(),
                new ObjectTypeSpecBuilder(),
                new StringArrayTypeSpecBuilder(),
                new RuntimeTypeSpecBuilder(),
                new ArrayOfPrimitiveTypeSpecBuilder(),
                new ArrayOfObjectTypeSpecBuilder()
            };
        }

        public TypeSpec GetTypeSpecFor(byte typeTag, BinaryReader binaryReader)
        {
            Logger.Debug("Getting TypeSpec for typeTag: '" + typeTag + "' at position: '" + binaryReader.BaseStream.Position + "'.");

            return GetTypeSpecBuilderFor(typeTag).BuildUsing(binaryReader);
        }

        private TypeSpecBuilder GetTypeSpecBuilderFor(byte typeTag)
        {
            var reader = _typeSpecBuilders.FirstOrDefault(x => x.CanRead(typeTag));

            if (reader == null) throw new ArgumentException("No type spec reader for type tag " + typeTag + ".", "typeTag");

            Logger.Debug("Found TypeSpecBuilder '" + reader.GetType().FullName + "'.");
            return reader;
        }
    }
}