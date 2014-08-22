using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ExternalObjectPartReader : ObjectReaderBase
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof (ExternalObjectPartReader));

        public ExternalObjectPartReader(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
            : base(partProvider, primitiveTypeReader)
        {
        }

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            logger.InfoFormat("Starting Read at: '{0}' -->.", binaryReader.BaseStream.Position);
            uint id = binaryReader.ReadUInt32();
            logger.DebugFormat("ObjectId: '{0}'.", id);
            string name = binaryReader.ReadString();
            logger.DebugFormat("Name: '{0}'.", name);
            uint fieldCount = binaryReader.ReadUInt32();
            logger.DebugFormat("fieldCount: '{0}'.", fieldCount);

            var fieldNames = new List<string>((int) fieldCount);

            foreach (int i in Enumerable.Range(0, Convert.ToInt32(fieldCount)))
            {
                string str = binaryReader.ReadString();
                fieldNames.Add(str);
                logger.DebugFormat("field: '{0}', name: '{1}'.", i, name);
            }

            List<TypeSpec> typeSpecs = ReadTypeSpecs(binaryReader, fieldCount);
            uint assemblyId = binaryReader.ReadUInt32();
            logger.DebugFormat("assemblyId: '{0}'.", assemblyId);

            var nodes = new List<Node>();
            foreach (TypeSpec current in typeSpecs)
            {
                nodes.Add(GetNodeBy(binaryReader, current, context));
            }

            var fieldNodes = new List<FieldNode>();

            foreach (int i in Enumerable.Range(0, fieldNames.Count))
            {
                fieldNodes.Add(new FieldNode(fieldNames[i], nodes[i], typeSpecs[i]));
            }

            logger.InfoFormat("<-- End Read at: '{0}'.", binaryReader.BaseStream.Position);
            return new ObjectNode(id, name, assemblyId, fieldNodes);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 5;
        }
    }
}