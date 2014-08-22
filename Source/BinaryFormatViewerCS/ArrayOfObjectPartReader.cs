using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfObjectPartReader : PartReader
    {
        private readonly PartProvider _partProvider;

        public ArrayOfObjectPartReader(PartProvider partProvider)
        {
            _partProvider = partProvider;
        }

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            uint objectId = binaryReader.ReadUInt32();
            uint numberOfElements = binaryReader.ReadUInt32();
            var elements = new List<Node>();

            foreach (int i in Enumerable.Range(0, Convert.ToInt32(numberOfElements)))
            {
                elements.Add(_partProvider.ReadNextPart(binaryReader, context));
            }

            return new ArrayOfObjectNode(objectId, elements);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 16;
        }
    }
}