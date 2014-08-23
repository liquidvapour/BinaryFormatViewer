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

            var metaDataObject = (IHaveTypeSpecs) context.ExistingObjects[typeMetaDataObjectId];

            uint assemblyId = 0U;
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

            var fieldData = Enumerable.Range(0, metaDataObject.Fields.Count)
                .Select(i => new FieldNode(metaDataObject.Fields[i].Name, GetNodeBy(binaryReader, metaDataObject.Fields[i].TypeSpec, context), metaDataObject.Fields[i].TypeSpec))
                .ToList();


            return new ObjectNode(id, metaDataObject.Name, assemblyId, fieldData);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 1;
        }
    }
}