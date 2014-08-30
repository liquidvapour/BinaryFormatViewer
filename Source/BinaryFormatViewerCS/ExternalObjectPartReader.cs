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
        private static readonly ILog Logger = LogManager.GetLogger(typeof (ExternalObjectPartReader));

        public ExternalObjectPartReader(PartProvider partProvider, PrimitiveTypeReader primitiveTypeReader)
            : base(partProvider, primitiveTypeReader)
        {
        }

        public override Node Read(BinaryReader binaryReader, ReadContext context)
        {
            Logger.InfoFormat("Starting Read at: '{0}' -->.", binaryReader.BaseStream.Position);
            uint id = binaryReader.ReadUInt32();
            Logger.DebugFormat("ObjectId: '{0}'.", id);
            string name = binaryReader.ReadString();
            Logger.DebugFormat("Name: '{0}'.", name);
            uint fieldCount = binaryReader.ReadUInt32();
            Logger.DebugFormat("fieldCount: '{0}'.", fieldCount);

            var fieldNames = new List<string>((int) fieldCount);

            foreach (int i in Enumerable.Range(0, Convert.ToInt32(fieldCount)))
            {
                string str = binaryReader.ReadString();
                fieldNames.Add(str);
                Logger.DebugFormat("field: '{0}', name: '{1}'.", i, name);
            }

            List<TypeSpec> typeSpecs = ReadTypeSpecs(binaryReader, fieldCount);
            uint assemblyId = binaryReader.ReadUInt32();
            Logger.DebugFormat("assemblyId: '{0}'.", assemblyId);

            var nodes = typeSpecs.Select(current => GetNodeBy(binaryReader, current, context)).ToList();

            var fieldNodes = Enumerable.Range(0, fieldNames.Count).Select(i => new FieldNode(fieldNames[i], nodes[i], typeSpecs[i])).ToList();

            Logger.InfoFormat("<-- End Read at: '{0}'.", binaryReader.BaseStream.Position);
            return new ObjectNode(id, name, assemblyId, fieldNodes);
        }

        public override bool CanRead(int partCode)
        {
            return partCode == 5;
        }
    }
}