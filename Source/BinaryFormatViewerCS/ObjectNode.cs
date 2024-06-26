﻿using System.Collections.Generic;

namespace BinaryFormatViewer
{
    public class ObjectNode : RuntimeObjectNode
    {
        public ObjectNode(uint id, string name, uint assemblyId, List<FieldNode> fields)
            : base(id, name, fields, new AssemblyRefNode(assemblyId))
        {
        }
    }
}