using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BinaryFormatViewer
{
    [Serializable]
    public class ExternalObjectPartReader : ObjectReaderBase
    {
        private static ILog logger = LogManager.GetLogger(typeof(ExternalObjectPartReader));

        public ExternalObjectPartReader(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
            : base(partProvider, primitiveTypeReader)
        {
        }

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            logger.InfoFormat("Starting Read at: '{0}' -->.", (object)binaryReader.BaseStream.Position);
            uint id = binaryReader.ReadUInt32();
            logger.DebugFormat("ObjectId: '{0}'.", (object)id);
            string name = binaryReader.ReadString();
            logger.DebugFormat("Name: '{0}'.", (object)name);
            uint fieldCount = binaryReader.ReadUInt32();
            logger.DebugFormat("fieldCount: '{0}'.", (object)fieldCount);

            List<string> fieldNames = new List<string>((int)fieldCount);

            foreach (var i in Enumerable.Range(0,  Convert.ToInt32(fieldCount)))
            {
                string str = binaryReader.ReadString();
                fieldNames.Add(str);
                logger.DebugFormat("field: '{0}', name: '{1}'.", i, name);
            }

            List<TypeSpec> typeSpecs = this.ReadTypeSpecs(binaryReader, fieldCount);
            uint assemblyId = binaryReader.ReadUInt32();
            ExternalObjectPartReader.logger.DebugFormat("assemblyId: '{0}'.", assemblyId);

            List<Node> nodes = new List<Node>();
            foreach (var current in typeSpecs)
            {
                nodes.Add(this.GetNodeBy(binaryReader, current, context));
            }

            List<FieldNode> fieldNodes = new List<FieldNode>();

            foreach (var i in Enumerable.Range(0, fieldNames.Count))
            {
                fieldNodes.Add(new FieldNode(fieldNames[i], nodes[i], typeSpecs[i]));
            }

            ExternalObjectPartReader.logger.InfoFormat("<-- End Read at: '{0}'.", (object)binaryReader.BaseStream.Position);
            return new ObjectNode(id, name, assemblyId, fieldNodes);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 5;
        }
    }
}
