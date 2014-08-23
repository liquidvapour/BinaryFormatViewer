using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfStringPartReader : PartReader
    {
        private readonly PartProvider _partProvider;

        public ArrayOfStringPartReader(PartProvider partProvider)
        {
            _partProvider = partProvider;
        }

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            uint objectId = binaryReader.ReadUInt32();
            uint numberOfElements = binaryReader.ReadUInt32();

            var elements = Enumerable.Range(0, Convert.ToInt32(numberOfElements))
                .Select(i => _partProvider.ReadNextPart(binaryReader, context))
                .ToList();

            return new ArrayOfStringNode(objectId, elements);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 17;
        }
    }
}