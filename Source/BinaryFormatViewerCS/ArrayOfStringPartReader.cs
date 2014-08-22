using System;
using System.Collections.Generic;
using System.IO;

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
            uint num1 = binaryReader.ReadUInt32();
            var elements = new List<Node>();
            int num2 = 0;
            var num3 = (int) num1;
            if (num3 < 0)
                throw new ArgumentOutOfRangeException("max");

            while (num2 < num3)
            {
                ++num2;
                elements.Add(_partProvider.ReadNextPart(binaryReader, context));
            }

            return new ArrayOfStringNode(objectId, elements);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 17;
        }
    }
}