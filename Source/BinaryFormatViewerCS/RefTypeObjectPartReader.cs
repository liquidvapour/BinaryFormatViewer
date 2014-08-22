using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryFormatViewer
{
    [Serializable]
    
    public class RefTypeObjectPartReader : ObjectReaderBase
    {
        public RefTypeObjectPartReader(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
            : base(partProvider, primitiveTypeReader)
        {
        }

        //TODO: rewrite to foreach.
        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            uint id = binaryReader.ReadUInt32();
            uint typeMetaDataObjectId = binaryReader.ReadUInt32();

            var metaDataObject = (IHaveTypeSpecs)context.ExistingObjects[typeMetaDataObjectId];

            var assemblyId = 0U;
            var assemblyRefNode = metaDataObject.Assembly as AssemblyRefNode;
            if (assemblyRefNode != null)
            {
                assemblyId = assemblyRefNode.Id;
            }
            else
            {
                var assemblyInfo = metaDataObject.Assembly as AssemblyNode;
                if (assemblyInfo != null) assemblyId = assemblyInfo.Id;
            }

            Dictionary<string, Node> fieldData = new Dictionary<string, Node>();

            foreach (var i in Enumerable.Range(0, metaDataObject.Fields.Count))
            {
                fieldData.Add(metaDataObject.Fields[i].Name, this.GetNodeBy(binaryReader, metaDataObject.Fields[i].TypeSpec, context));
            }

            return new ObjectNode(id, metaDataObject.Name, assemblyId, metaDataObject.Fields);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 1;
        }
    }
}
