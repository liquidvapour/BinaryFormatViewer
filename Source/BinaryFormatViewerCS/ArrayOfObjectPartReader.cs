using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ArrayOfObjectPartReader : PartReader
    {
        private PartProvider _partProvider;

        public ArrayOfObjectPartReader(PartProvider partProvider)
        {
            this._partProvider = partProvider;
        }

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            uint objectId = binaryReader.ReadUInt32();
            uint numberOfElements = binaryReader.ReadUInt32();
            List<Node> elements = new List<Node>();

            foreach (var i in Enumerable.Range(0, Convert.ToInt32(numberOfElements)))
            {
                elements.Add(this._partProvider.ReadNextPart(binaryReader, context));
            }

            return (Node)new ArrayOfObjectNode(objectId, elements);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 16;
        }
    }
}
